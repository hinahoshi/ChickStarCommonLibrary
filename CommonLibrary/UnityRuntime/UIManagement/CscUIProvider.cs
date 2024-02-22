using System;
using System.Collections.Generic;
using System.Linq;
using ChickStar.CommonLibrary.UnityRuntime.UIManagement.UIPreset;
using ChickStar.CommonLibrary.UnityRuntime.UnityComponents;
using ChickStar.CommonLibrary.UnityRuntime.UnityComponents.Dialog;
using UnityEngine;
using UnityEngine.Serialization;

namespace ChickStar.CommonLibrary.UnityRuntime.UIManagement
{
    [RequireComponent(typeof(Canvas))]
    public class CscUIProvider : SingletonMonoBehaviour<CscUIProvider>
    {
        private Canvas _canvas;
        public Canvas Canvas => _canvas;
        public UIPresetCatalog uiPresetCatalog;

        protected override bool DontDestroy => false;

        public string loadPresetsName = UIPreset.UIPreset.DefaultName;

        private readonly Dictionary<UIPreset.UIPreset.PresetType, CscUI> _uiCaches= new();

        private UIPreset.UIPreset _currentPreset;

        protected override void Initialize()
        {
            _canvas = GetComponent<Canvas>();
            _currentPreset = uiPresetCatalog.presets.FirstOrDefault(x => x.presetName == loadPresetsName);
        }

        public DialogView ShowDialog()
        {
            if (_uiCaches.TryGetValue(UIPreset.UIPreset.PresetType.Dialog, out var cache))
            {
                cache.Activate();
                return (DialogView)cache;
            }

            if (_currentPreset == null)
            {
                return null;
            }

            var dialogPreset = _currentPreset.dialog;
            if (dialogPreset == null)
            {
                return null;
            }

            var instance = Instantiate(dialogPreset.dialogView, _canvas.transform);
            _uiCaches.Add(UIPreset.UIPreset.PresetType.Dialog, instance);
            return instance;
        }
    }
}