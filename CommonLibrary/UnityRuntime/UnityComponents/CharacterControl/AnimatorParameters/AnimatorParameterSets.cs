using System.Collections.Generic;
using UnityEngine;

namespace ChickStar.CommonLibrary.UnityRuntime.UnityComponents.CharacterControl.AnimatorParameters
{
    [CreateAssetMenu(menuName = "Create AnimatorParameters", fileName = "AnimatorParameter", order = 0)]
    public class AnimatorParameterSets : ScriptableObject
    {
        public List<AnimatorParameter> parameters;
    }
}