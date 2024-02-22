using ChickStar.CommonLibrary.Editor.Common;
using ChickStar.CommonLibrary.Editor.Common.DrawUtils.Drawer;
using ChickStar.CommonLibrary.UnityRuntime.UnityComponents.CharacterControl.AnimatorParameters;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace ChickStar.CommonLibrary.Editor.CustomInspectors.CharacterControl
{
    [CustomEditor(typeof(AnimatorParameterSets))]
    public class AnimatorParameterSetsInspectorDrawer : CustomInspectorBase<AnimatorParameterSets>
    {
        private SerializedProperty _parametersProperty;
        private ReorderableList _reorderableList;

        private void OnEnable()
        {
            InitializeSerializable();
        }

        protected override void DrawGui()
        {
            _reorderableList.DoLayoutList();
        }

        protected override void InitializeSerializable()
        {
            base.InitializeSerializable();
            _parametersProperty ??= serializedObject.FindProperty(nameof(Target.parameters));

            _reorderableList ??= new ReorderableList(serializedObject, _parametersProperty);

            InitializeReorderableList();
        }

        private void InitializeReorderableList()
        {
            _reorderableList.RegisterDraw(_parametersProperty,
                drawElementCallback: DrawElementCallback
            );
        }

        private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            var prevHeight = rect.height;
            var prevWidth = rect.width;
            var prevLabelWidth = EditorGUIUtility.labelWidth;
            rect.height = EditorGUIUtility.singleLineHeight;

            EditorGUIUtility.labelWidth = 42f;

            var element = Target.parameters[index];
            rect.y += EditorGUIUtility.singleLineHeight;
            Target.parameters[index].parameterName = EditorGUI.TextField(rect, "Name", element.parameterName);

            rect.y += EditorGUIUtility.singleLineHeight;
            element.parameterRoll =
                (AnimatorParameter.ParameterRoll)EditorGUI.EnumPopup(rect, "Roll", element.parameterRoll);

            rect.height = prevHeight;
            rect.width = prevWidth;
            EditorGUIUtility.labelWidth = prevLabelWidth;
        }
    }
}