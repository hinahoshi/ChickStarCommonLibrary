using System;
using UnityEngine.UI;

namespace ChickStar.CommonLibrary.UnityRuntime.UIManagement
{
    public abstract class ButtonControlBase : SelectableUIControlBase<Button>
    {
        public override void Register(Button selectable)
        {
            selectable.onClick.AddListener(OnClick);
        }

        public override void UnRegister(Button selectable)
        {
            selectable.onClick.RemoveListener(OnClick);
            
        }
        public abstract void OnClick();
    }
}