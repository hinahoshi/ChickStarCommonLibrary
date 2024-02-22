using System;
using System.Collections.Generic;
using ChickStar.CommonLibrary.Editor.Common;
using ChickStar.CommonLibrary.Editor.Common.DrawUtils.Drawer;
using ChickStar.CommonLibrary.Editor.Translation;
using ChickStar.CommonLibrary.Runtime.Utils.Serialization;
using ChickStar.CommonLibrary.UnityRuntime.UIManagement;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

namespace ChickStar.CommonLibrary.Editor.CustomInspectors.UINavigation
{
    [CustomEditor(typeof(UINavigator))]
    public class UINavigatorInspectorDrawer : CustomInspectorBase<UINavigator>
    {
        private interface INavigationsProperty
        {
            public void Initialize();
            public void DoLayout();
        }

        [Serializable]
        private class NavigationsProperty<T> : INavigationsProperty where T : Selectable
        {
            private readonly List<UINavigation<T>> _navigations;
            private readonly SerializedProperty _serializedProperty;
            public readonly ReorderableList ReorderableList;
            private readonly string _label;

            public NavigationsProperty(SerializedObject serializedObject, string propertyName,
                List<UINavigation<T>> navigations, string label)
            {
                _serializedProperty = serializedObject.FindProperty(propertyName);
                ReorderableList = new ReorderableList(serializedObject, _serializedProperty);
                _navigations = navigations;
                _label = label;
            }

            public void Initialize()
            {
                InitializeReorderableList();
                foreach (var navigation in _navigations)
                {
                    UpdateSelectableUI(navigation, navigation.selectableUI);
                }
            }

            public void DoLayout()
            {
                ReorderableList.DoLayoutList();
            }

            private void InitializeReorderableList()
            {
                ReorderableList.RegisterDraw(_serializedProperty,
                    drawElementCallback: (rect, index, isActive, isFocused) =>
                    {
                        DrawElementCallback(_navigations, rect, index, isActive, isFocused);
                    },
                    label: _label
                );
            }

            private void DrawElementCallback(List<UINavigation<T>> navigations, Rect rect, int index, bool isActive,
                bool isFocused)
            {
                var prevHeight = rect.height;
                var prevWidth = rect.width;
                var prevLabelWidth = EditorGUIUtility.labelWidth;
                rect.height = EditorGUIUtility.singleLineHeight;

                EditorGUIUtility.labelWidth = 42f;

                var element = navigations[index];
                DrawNavigationElement(rect, element);

                rect.height = prevHeight;
                rect.width = prevWidth;
                EditorGUIUtility.labelWidth = prevLabelWidth;
            }

            private void DrawNavigationElement(Rect rect, UINavigation<T> element, string label = "UI")
            {
                var newSelectableUi = (T)EditorGUI.ObjectField(rect, label, element.selectableUI, typeof(T), true);

                // selectableUIが更新されていれば、UIControlの取得を試みる
                if (element.selectableUI != newSelectableUi)
                {
                    UpdateSelectableUI(element, newSelectableUi);
                    // SelectableUIの更新
                    element.selectableUI = newSelectableUi;
                }

                if (element.uiControl is { Bodies: { Length: > 0 } })
                {
                    foreach (var body in element.uiControl.Bodies)
                    {
                        rect.y += EditorGUIUtility.singleLineHeight;
                        EditorGUI.LabelField(rect, body.GetType().Name);
                    }
                }
                else
                {
                    rect.y += EditorGUIUtility.singleLineHeight;
                    EditorGUI.LabelField(rect, Messages.Current["UINavigation.UIControlNotFound"]);
                }
            }

            private void UpdateSelectableUI(UINavigation<T> element, T newSelectableUi)
            {
                if (newSelectableUi != null)
                {
                    element.uiControl = new SerializableAbstractComponent<SelectableUIControlBase<T>>
                    {
                        gameObject = newSelectableUi.gameObject,
                    };
                }
                else if (newSelectableUi == null)
                {
                    element.uiControl = null;
                }

                // SelectableUIの更新
                element.selectableUI = newSelectableUi;
            }
        }

        private enum Tab
        {
            Button,
            InputField
        }

        private readonly string[] _tabs = { "Button", "InputField" };
        private int _tabIndex;

        private readonly Dictionary<Tab, INavigationsProperty> _tabToNavigationsPropertyMap = new();

        protected override void InitializeSerializable()
        {
            base.InitializeSerializable();

            _tabToNavigationsPropertyMap.Clear();

            var buttonNavigationsProperty = new NavigationsProperty<Button>(
                serializedObject: serializedObject,
                propertyName: nameof(Target.buttonNavigations),
                navigations: Target.buttonNavigations,
                label: "Buttons"
            );
            _tabToNavigationsPropertyMap.Add(Tab.Button, buttonNavigationsProperty);

            var inputFieldNavigationsProperty = new NavigationsProperty<InputField>(
                serializedObject: serializedObject,
                propertyName: nameof(Target.inputFieldNavigations),
                navigations: Target.inputFieldNavigations,
                label: "InputFields"
            );
            _tabToNavigationsPropertyMap.Add(Tab.InputField, inputFieldNavigationsProperty);


            foreach (var kvp in _tabToNavigationsPropertyMap)
            {
                kvp.Value.Initialize();
            }
        }

        protected override void DrawGui()
        {
            using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                _tabIndex = GUILayout.Toolbar(_tabIndex, _tabs, new GUIStyle(EditorStyles.toolbarButton),
                    GUI.ToolbarButtonSize.FitToContents);
            }

            if (_tabToNavigationsPropertyMap.TryGetValue((Tab)_tabIndex, out var property))
            {
                property.DoLayout();
            }
        }
    }
}