using UnityEngine;
using UnityEngine.UI;

namespace ChickStar.CommonLibrary.UnityRuntime.UIManagement
{
    public abstract class SelectableUIControlBase<T> : MonoBehaviour where T : Selectable
    {
        public abstract void Register(T selectable);
        public abstract void UnRegister(T selectable);
    }
}