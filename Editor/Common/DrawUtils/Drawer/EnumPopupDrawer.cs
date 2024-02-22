using System;
using System.Collections.Generic;
using ChickStar.CommonLibrary.Runtime.Utils;
using UnityEditor;
using UnityEngine;

namespace ChickStar.CommonLibrary.Editor.Common.DrawUtils.Drawer
{
    [Serializable]
    public class EnumPopupDrawer<T> where T : Enum
    {
        public string[] displayOptions;
        public int currentIndex;

        public EnumPopupDrawer(int defaultIndex = 0, Dictionary<T, string> displayOverrideOption = null)
        {
            var enumValues = Enum.GetValues(typeof(T));
            displayOptions = new string[enumValues.Length];

            foreach (T enumValue in enumValues)
            {
                var index = Array.IndexOf(enumValues, enumValue);
                if (displayOverrideOption != null && displayOverrideOption.TryGetValue(enumValue,
                        out var option))
                {
                    // displayOverrideOptionが存在し、中身が存在するならば、表示名だけ変える
                    if (!string.IsNullOrEmpty(option))
                    {
                        displayOptions[index] = option;
                        continue;
                    }
                }

                displayOptions[index] = enumValue.ToString();
            }

            currentIndex = defaultIndex;
        }

        public int Draw(string label)
        {
            currentIndex = EditorGUILayout.Popup(label, currentIndex, displayOptions);
            return currentIndex;
        }
    }
}