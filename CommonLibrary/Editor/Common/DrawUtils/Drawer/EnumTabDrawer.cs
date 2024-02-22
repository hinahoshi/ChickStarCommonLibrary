using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ChickStar.CommonLibrary.Editor.Common.DrawUtils.Drawer
{
    [Serializable]
    public class EnumTabDrawer<T> where T : Enum
    {
        public string[] tabs;
        public int currentIndex;

        public EnumTabDrawer(int defaultIndex = 0)
        {
            var enumValues = Enum.GetValues(typeof(T));
            tabs = new string[enumValues.Length];

            foreach (T enumValue in enumValues)
            {
                var index = Array.IndexOf(enumValues, enumValue);
                tabs[index] = enumValue.ToString();
            }

            currentIndex = defaultIndex;
        }

        public int Draw(DrawDirection direction = DrawDirection.Horizontal)
        {
            GUI.Scope scope = direction switch
            {
                DrawDirection.Horizontal => new EditorGUILayout.HorizontalScope(EditorStyles.toolbar),
                DrawDirection.Vertical => new EditorGUILayout.VerticalScope(EditorStyles.toolbar),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };

            using (scope)
            {
                currentIndex = GUILayout.Toolbar(
                    currentIndex,
                    tabs,
                    DrawUtil.ToolBarStyle,
                    GUI.ToolbarButtonSize.FitToContents
                );
            }

            return currentIndex;
        }
    }
}