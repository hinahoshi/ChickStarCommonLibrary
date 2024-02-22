using ChickStar.CommonLibrary.Runtime.UnityDefines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChickStar.CommonLibrary.UnityRuntime.UnityComponents.CharacterControl.Behaviours
{
    public class SimpleGamepadModule : CscCharacterModule
    {
        public override InitializationTiming InitializationTiming => InitializationTiming.Awake;
        private bool _isLeftStickActuated;

        public override void OnUpdate()
        {
            if (Gamepad.current == null)
            {
                return;
            }

            CheckJump();
            CheckMove();
        }

        private void CheckJump()
        {
            if (!Behaviour.IsGrounded)
            {
                return;
            }

            if (Gamepad.current.buttonNorth.wasPressedThisFrame)
            {
                Behaviour.Jump();
            }
        }

        private void CheckMove()
        {
            var leftStick = Gamepad.current.leftStick;
            if (leftStick.IsActuated() && !_isLeftStickActuated)
            {
                _isLeftStickActuated = true;
                Behaviour.StartMoving();
                return;
            }

            if (!leftStick.IsActuated() && _isLeftStickActuated)
            {
                _isLeftStickActuated = false;
                Behaviour.EndMoving();
                return;
            }

            if (_isLeftStickActuated)
            {
                Behaviour.Moving(leftStick.ReadValue());
            }
        }

        void OnGUI()
        {
            if (Gamepad.current == null) return;

            GUILayout.Label($"leftStick: {Gamepad.current.leftStick.ReadValue()}");
            GUILayout.Label($"buttonNorth: {Gamepad.current.buttonNorth.isPressed}");
            GUILayout.Label($"buttonSouth: {Gamepad.current.buttonSouth.isPressed}");
            GUILayout.Label($"buttonEast: {Gamepad.current.buttonEast.isPressed}");
            GUILayout.Label($"buttonWest: {Gamepad.current.buttonWest.isPressed}");
            GUILayout.Label($"leftShoulder: {Gamepad.current.leftShoulder.ReadValue()}");
            GUILayout.Label($"leftTrigger: {Gamepad.current.leftTrigger.ReadValue()}");
            GUILayout.Label($"rightShoulder: {Gamepad.current.rightShoulder.ReadValue()}");
            GUILayout.Label($"rightTrigger: {Gamepad.current.rightTrigger.ReadValue()}");
        }
    }
}