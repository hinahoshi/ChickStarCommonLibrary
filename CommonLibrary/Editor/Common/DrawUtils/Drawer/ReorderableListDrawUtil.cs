using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace ChickStar.CommonLibrary.Editor.Common.DrawUtils.Drawer
{
    public static class ReorderableListDrawUtil
    {
        public delegate void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused);

        public static void RegisterDraw(this ReorderableList reorderableList,
            SerializedProperty listProperty,
            string label = "Reorderable List",
            float elementHeight = 100f,
            DrawElementCallback drawElementCallback = null
        )
        {
            reorderableList.drawHeaderCallback = rect => { SimpleDrawHeader(rect, label); };
            reorderableList.elementHeight = elementHeight;

            if (drawElementCallback != null)
            {
                reorderableList.drawElementCallback = drawElementCallback.Invoke;
            }
        }

        private static void SimpleDrawHeader(Rect rect, string label)
        {
            EditorGUI.LabelField(rect, label);
        }
    }
}