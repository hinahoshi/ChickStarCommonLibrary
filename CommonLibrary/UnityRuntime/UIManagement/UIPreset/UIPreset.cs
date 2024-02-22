using System;
using ChickStar.CommonLibrary.UnityRuntime.UnityComponents.Dialog;
using UnityEngine;

namespace ChickStar.CommonLibrary.UnityRuntime.UIManagement.UIPreset
{
    [Serializable]
    public class UIPreset
    {
        public enum PresetType
        {
            Dialog
        }
        
        public const string DefaultName = "DefaultPreset";

        [Serializable]
        public class DialogPreset
        {
            public DialogView dialogView;
        }

        public string presetName = DefaultName;
        public DialogPreset dialog;
    }
}