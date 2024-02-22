using System;
using UnityEngine;

namespace ChickStar.CommonLibrary.UnityRuntime.UnityComponents.CharacterControl.AnimatorParameters
{
    [Serializable]
    public class AnimatorParameter
    {
        public enum ParameterRoll
        {
            MovingStatus,
            MovingParameter,

            JumpingStatus,

            AttackingStatus,

            FallingStatus,

            AnimationSpeed,
            Trigger,
        }

        public string parameterName;
        public ParameterRoll parameterRoll;

        public void Set<T>(Animator animator, T value)
        {
            switch (value)
            {
                case int i:
                    Set(animator, i);
                    break;
                case float f:
                    Set(animator, f);
                    break;
                case bool b:
                    Set(animator, b);
                    break;
                default:
                    throw new Exception($"Not supported type:{typeof(T)}");
            }
        }

        private void Set(Animator animator, int value)
        {
            animator.SetInteger(parameterName, value);
        }

        private void Set(Animator animator, float value)
        {
            animator.SetFloat(parameterName, value);
        }

        private void Set(Animator animator, bool value)
        {
            animator.SetBool(parameterName, value);
        }

        public void SetTrigger(Animator animator)
        {
            animator.SetTrigger(parameterName);
        }
    }
}