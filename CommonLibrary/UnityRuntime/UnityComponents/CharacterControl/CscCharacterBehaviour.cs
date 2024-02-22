using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ChickStar.CommonLibrary.Runtime.UnityDefines;
using ChickStar.CommonLibrary.Runtime.Utils.Serialization;
using ChickStar.CommonLibrary.UnityRuntime.UnityComponents.CharacterControl.AnimatorParameters;
using ChickStar.CommonLibrary.UnityRuntime.UnityComponents.CharacterControl.Behaviours;
using UnityEngine;
using UnityEngine.Serialization;

namespace ChickStar.CommonLibrary.UnityRuntime.UnityComponents.CharacterControl
{
    public enum JumpingState
    {
        Landing = 0,
        Jumping = 1,
        Falling = 2,
    }

    [RequireComponent(typeof(CharacterController))]
    public class CscCharacterBehaviour : MonoBehaviour
    {
        [SerializeField] private float moveMultiplier = 6.5f;
        [SerializeField] private float jumpMultiplier = 2.5f;

        private CharacterController _characterController;
        private Vector3 _moveDirection = Vector3.zero;
        [SerializeField] public List<SerializableAbstractComponent<CscCharacterModule>> modules;
        private readonly List<CscCharacterModule> _cscCharacterModules = new();

        [SerializeField] private Animator animator;
        [SerializeField] private RuntimeAnimatorController animatorController;
        [SerializeField] private float defaultAnimationSpeed = 1.0f;

        public bool IsGrounded => _characterController != null && _characterController.isGrounded;

        public AnimatorParameterSets animatorParameterSets;
        private Dictionary<AnimatorParameter.ParameterRoll, List<AnimatorParameter>> _groupedAnimatorParameters;


        private bool _isMoving;

        private bool IsMoving
        {
            get => _isMoving;
            set
            {
                SetAnimatorParameter(AnimatorParameter.ParameterRoll.MovingStatus, value);
                _isMoving = value;
            }
        }

        private bool _isJumping;

        private bool _isFalling;

        private Vector3 _lookDirection;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            InitializeAnimatorParameterSets();

            foreach (var module in modules)
            {
                var body = module.Body;
                if (body == null)
                {
                    continue;
                }

                if (module.gameObject == null)
                {
                    continue;
                }

                _cscCharacterModules.Add(body);
            }

            InitializeModules(InitializationTiming.Awake);
        }

        private void OnEnable()
        {
            InitializeModules(InitializationTiming.OnEnable);
        }

        private void Start()
        {
            InitializeModules(InitializationTiming.Start);
        }

        private void InitializeModules(InitializationTiming timing)
        {
            var initializeTargets =
                _cscCharacterModules.Where(x => x.InitializationTiming == timing);
            foreach (var initializeTarget in initializeTargets)
            {
                initializeTarget.Initialize(this);
            }
        }

        private void InitializeAnimatorParameterSets()
        {
            _groupedAnimatorParameters = new Dictionary<AnimatorParameter.ParameterRoll, List<AnimatorParameter>>();
            foreach (var parameter in animatorParameterSets.parameters)
            {
                if (!_groupedAnimatorParameters.TryGetValue(parameter.parameterRoll, out var value))
                {
                    value = new List<AnimatorParameter>();
                    _groupedAnimatorParameters.Add(parameter.parameterRoll, value);
                }

                value.Add(parameter);
            }
        }

        public void SetCharacter(GameObject loadTarget)
        {
            loadTarget.transform.parent = transform;
            loadTarget.transform.localPosition = Vector3.zero;
            loadTarget.transform.localRotation = Quaternion.identity;

            animator = loadTarget.GetComponent<Animator>();
            if (animator == null)
            {
                enabled = false;
                throw new InvalidDataException("Animator is null");
            }

            animator.runtimeAnimatorController = animatorController;

            ChangeAnimationSpeed(defaultAnimationSpeed);
        }

        private void Update()
        {
            InFall();
            foreach (var module in _cscCharacterModules)
            {
                module.OnUpdate();
            }
        }

        private void InFall()
        {
            if (_characterController.isGrounded)
            {
                if (_moveDirection.y < -0.1f)
                {
                    _moveDirection.y = -0.1f;
                }

                // ジャンプ中の場合、地面に接したタイミングでジャンプを終了する

                if (_isJumping || _isFalling)
                {
                    SetAnimatorParameter(
                        AnimatorParameter.ParameterRoll.JumpingStatus,
                        (int)JumpingState.Landing,
                        needTrigger: true
                    );
                }

                if (_isJumping)
                {
                    _isJumping = false;
                }

                if (_isFalling)
                {
                    _isFalling = false;
                }
            }
            else if (!_characterController.isGrounded)
            {
                if (_moveDirection.y > -5)
                {
                    _moveDirection.y -= 0.5f;
                }

                if (!_isJumping && !_isFalling)
                {
                    // 地面に接していない場合、落下を開始する
                    _isFalling = true;
                    SetAnimatorParameter(
                        AnimatorParameter.ParameterRoll.JumpingStatus,
                        (int)JumpingState.Falling,
                        needTrigger: true
                    );
                }


                if (!IsMoving)
                {
                    ControlMove();
                }
            }
        }

        private void Look()
        {
            // _moveDirectionの向き見る
            _lookDirection.x = _moveDirection.x;
            _lookDirection.z = _moveDirection.z;
            if (_lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(_lookDirection);
            }
        }

        public void Jump()
        {
            _moveDirection.y = 8.0f * jumpMultiplier;
            SetAnimatorParameter(
                AnimatorParameter.ParameterRoll.JumpingStatus,
                (int)JumpingState.Jumping,
                needTrigger: true
            );
            _isJumping = true;

            if (!IsMoving)
            {
                ControlMove();
            }
        }


        public void StartMoving()
        {
            IsMoving = true;
        }

        public void Moving(Vector2 value)
        {
            _moveDirection.x = value.x * moveMultiplier;
            _moveDirection.z = value.y * moveMultiplier;

            ControlMove();
            Look();

            _moveDirection.x = 0;
            _moveDirection.z = 0;
        }

        public void EndMoving()
        {
            IsMoving = false;
        }

        private void ControlMove()
        {
            SetAnimatorParameter(AnimatorParameter.ParameterRoll.MovingParameter, _moveDirection.magnitude);
            _characterController.Move(_moveDirection * Time.deltaTime);
        }

        public void ChangeAnimationSpeed(float value)
        {
            SetAnimatorParameter(AnimatorParameter.ParameterRoll.AnimationSpeed, value);
        }

        private void SetAnimatorParameter<T>(AnimatorParameter.ParameterRoll parameterRoll, T value,
            bool needTrigger = false) where T : unmanaged
        {
            if (animator == null)
            {
                return;
            }

            if (!_groupedAnimatorParameters.TryGetValue(parameterRoll, out var parameters))
            {
                return;
            }

            foreach (var parameter in parameters)
            {
                parameter.Set(animator, value);
            }

            if (needTrigger)
            {
                SetAnimatorTriggerParameter();
            }
        }

        private void SetAnimatorTriggerParameter()
        {
            if (!_groupedAnimatorParameters.TryGetValue(AnimatorParameter.ParameterRoll.Trigger, out var parameters))
            {
                return;
            }

            foreach (var parameter in parameters)
            {
                parameter.SetTrigger(animator);
            }
        }
    }
}