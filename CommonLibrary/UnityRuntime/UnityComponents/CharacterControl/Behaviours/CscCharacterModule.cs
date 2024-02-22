using ChickStar.CommonLibrary.Runtime.UnityDefines;
using UnityEngine;

namespace ChickStar.CommonLibrary.UnityRuntime.UnityComponents.CharacterControl.Behaviours
{
    public abstract class CscCharacterModule : MonoBehaviour
    {
        protected CscCharacterBehaviour Behaviour;
        public abstract InitializationTiming InitializationTiming { get; }

        public virtual void Initialize(CscCharacterBehaviour behaviour)
        {
            Behaviour = behaviour;
        }

        public abstract void OnUpdate();
    }
}