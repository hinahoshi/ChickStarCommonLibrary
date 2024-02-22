using UnityEditor;
using UnityEngine;

namespace ChickStar.CommonLibrary.Editor.Common.DrawUtils
{
    public partial class DrawUtil
    {
        private static GUIStyle _toolBarStyle;
        public static GUIStyle ToolBarStyle => _toolBarStyle ??= new GUIStyle(EditorStyles.toolbarButton);
    }
}