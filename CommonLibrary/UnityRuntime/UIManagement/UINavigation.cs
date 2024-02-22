using System;
using ChickStar.CommonLibrary.Runtime.Utils.Serialization;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ChickStar.CommonLibrary.UnityRuntime.UIManagement
{
    [Serializable]
    public class UINavigation<T> where T : Selectable
    {
        public T selectableUI;
        public SerializableAbstractComponent<SelectableUIControlBase<T>> uiControl;

        public void Register()
        {
            if (uiControl.Bodies != null)
            {
                foreach (var body in uiControl.Bodies)
                {
                    body.Register(selectableUI);
                }
            }
        }

        public void UnRegister()
        {
            if (uiControl.Bodies != null)
            {
                foreach (var body in uiControl.Bodies)
                {
                    body.UnRegister(selectableUI);
                }
            }
        }
    }
}