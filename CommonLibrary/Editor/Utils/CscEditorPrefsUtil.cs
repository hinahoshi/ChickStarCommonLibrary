using System;
using ChickStar.CommonLibrary.Runtime.Logger;
using UnityEditor;

namespace ChickStar.CommonLibrary.Editor.Utils
{
    public class CscEditorPrefsUtil
    {
        private const string PrefsPrefix = "CSC";
        private const string UserPrefix = "CSC";

        public enum PrefsType
        {
            User,
            System,
            Develop
        }

        #region SetPrefs

        public static void SetPrefs(PrefsType prefsType, string key, object value)
        {
            var builtKey = BuildKey(prefsType, key);
            switch (value)
            {
                case string strValue:
                    EditorPrefs.SetString(builtKey, strValue);
                    break;
                case bool boolValue:
                    EditorPrefs.SetBool(builtKey, boolValue);
                    break;
                case int intValue:
                    EditorPrefs.SetInt(builtKey, intValue);
                    break;
                case float floatValue:
                    EditorPrefs.SetFloat(builtKey, floatValue);
                    break;
                default:
                    CscEasyLogger.LogError($"Failed to set EditorPrefs\n" +
                                           $"Unsupported type {value.GetType()} was specified for key {key}.");
                    break;
            }
        }

        public static void SetPrefs(PrefsType prefsType, string key, Enum value)
        {
            var intValue = Convert.ToInt32(value);
            SetPrefs(prefsType, key, intValue);
        }

        #endregion

        #region GetPrefs

        public static string GetPrefs(PrefsType prefsType, string key, string defaultValue = "")
        {
            var builtKey = BuildKey(prefsType, key);
            return EditorPrefs.GetString(builtKey, defaultValue);
        }

        public static int GetPrefs(PrefsType prefsType, string key, int defaultValue = 0)
        {
            var builtKey = BuildKey(prefsType, key);
            return EditorPrefs.GetInt(builtKey, defaultValue);
        }

        public static bool GetPrefs(PrefsType prefsType, string key, bool defaultValue = false)
        {
            var builtKey = BuildKey(prefsType, key);
            return EditorPrefs.GetBool(builtKey, defaultValue);
        }

        public static float GetPrefs(PrefsType prefsType, string key, float defaultValue = 0.0f)
        {
            var builtKey = BuildKey(prefsType, key);
            return EditorPrefs.GetFloat(builtKey, defaultValue);
        }

        public static T GetPrefs<T>(PrefsType prefsType, string key, T defaultValue) where T : Enum
        {
            var defaultIntValue = Convert.ToInt32(defaultValue);
            var resultIntValue = GetPrefs(prefsType, key, defaultIntValue);
            return (T)Enum.ToObject(typeof(T), resultIntValue);
        }

        #endregion

        #region DeletePrefs

        public static void DeletePrefs(PrefsType prefsType, string key)
        {
            var builtKey = BuildKey(prefsType, key);
            EditorPrefs.DeleteKey(builtKey);
        }

        #endregion

        private static string BuildKey(PrefsType prefsType, string key)
        {
            return $"{PrefsPrefix}_{GetPrefsTypeString(prefsType)}_{key}";
        }

        private static string GetPrefsTypeString(PrefsType prefsType)
        {
            return prefsType switch
            {
                PrefsType.User => "USER",
                PrefsType.System => "SYSTEM",
                PrefsType.Develop => "DEVELOP",
                _ => throw new ArgumentOutOfRangeException(nameof(prefsType), prefsType, null)
            };
        }
    }
}