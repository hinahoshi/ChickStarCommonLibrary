using ChickStar.CommonLibrary.Editor.Common.UnityIcon;
using UnityEditor;
using UnityEngine;

namespace ChickStar.CommonLibrary.Editor.Common.DrawUtils
{
    public static partial class DrawUtil
    {
        #region Button

        public static bool DrawAvailabilityButton(GUIContent content,
            ref bool isAvailable,
            GUIStyle style = null,
            params GUILayoutOption[] options)
        {
            using (new EditorGUI.DisabledScope(!isAvailable))
            {
                return GUILayout.Button(content, style ?? GUI.skin.button, options);
            }
        }

        public static bool DrawAvailabilityButton(string content,
            ref bool isAvailable,
            GUIStyle style = null,
            params GUILayoutOption[] options)
        {
            using (new EditorGUI.DisabledScope(!isAvailable))
            {
                return GUILayout.Button(content, style ?? GUI.skin.button, options);
            }
        }

        public static bool DrawAvailabilityButton(string content, GUIStyle style,
            ref bool isAvailable,
            params GUILayoutOption[] options)
        {
            using (new EditorGUI.DisabledScope(!isAvailable))
            {
                return GUILayout.Button(content, style, options);
            }
        }

        public static bool DrawIconButton(IconType iconType, ref bool isAvailable,
            params GUILayoutOption[] layoutOptions)
        {
            var iconGuiContent = UnityIconDrawer.GetIconGuiContent(iconType);
            return DrawAvailabilityButton(iconGuiContent, ref isAvailable, EditorStyles.miniButtonMid, layoutOptions);
        }

        public static bool DrawIconButton(IconType iconType,
            params GUILayoutOption[] layoutOptions)
        {
            var iconGuiContent = UnityIconDrawer.GetIconGuiContent(iconType);
            return GUILayout.Button(iconGuiContent, layoutOptions);
        }

        public static bool DrawIconButton(IconType iconType, GUIStyle style = null,
            params GUILayoutOption[] layoutOptions)
        {
            var iconGuiContent = UnityIconDrawer.GetIconGuiContent(iconType);
            return GUILayout.Button(iconGuiContent, style ?? EditorStyles.miniButtonMid, layoutOptions);
        }

        #endregion

        #region Field

        public static T DrawAvailabilityObjectField<T>(ref bool isAvailable,
            string label, T target, bool allowSceneObjects) where T : Object
        {
            using (new EditorGUI.DisabledScope(!isAvailable))
            {
                return (T)EditorGUILayout.ObjectField(label, target, typeof(T), allowSceneObjects);
            }
        }

        public static T DrawAvailabilityObjectField<T>(ref bool isAvailable,
            T target, bool allowSceneObjects) where T : Object
        {
            using (new EditorGUI.DisabledScope(!isAvailable))
            {
                return (T)EditorGUILayout.ObjectField(target, typeof(T), allowSceneObjects);
            }
        }

        #endregion

        #region Other

        public static void DrawHorizontalDivider(int indentLevel = -1, bool spacing = false)
        {
            var indent = EditorGUI.indentLevel;
            if (indentLevel != -1)
            {
                indent = indentLevel;
            }

            if (spacing)
            {
                EditorGUILayout.Space();
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(indent * EditorGUIUtility.singleLineHeight);
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
            }

            if (spacing)
            {
                EditorGUILayout.Space();
            }
        }

        #endregion
    }
}