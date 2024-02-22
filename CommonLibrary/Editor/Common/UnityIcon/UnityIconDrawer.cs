using ChickStar.CommonLibrary.Editor.Common.DrawUtils;
using UnityEditor;
using UnityEngine;

namespace ChickStar.CommonLibrary.Editor.Common.UnityIcon
{
    public static partial class UnityIconDrawer
    {
        public static void Draw(IconType iconType)
        {
            var guiContent = GetIconGuiContent(iconType);
            GUILayout.Box(guiContent, GUIStyle.none);
        }

        public static GUIContent GetIconGuiContent(IconType iconType)
        {
            return Map.TryGetValue(iconType, out var iconName)
                ? EditorGUIUtility.IconContent(iconName)
                : DrawUtil.EmptyGuiContent;
        }
    }
}