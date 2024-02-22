using System.Collections.Generic;
using UnityEngine;

namespace ChickStar.CommonLibrary.UnityRuntime.UIManagement.UIPreset
{
    [CreateAssetMenu(menuName = "Create UIPresetCatalog", fileName = "UIPresetCatalog", order = 0)]
    public class UIPresetCatalog : ScriptableObject
    {
        public List<UIPreset> presets;
    }
}