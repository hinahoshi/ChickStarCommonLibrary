using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ChickStar.CommonLibrary.Editor.Utils;
using UnityEditor;
using UnityEngine;

namespace ChickStar.CommonLibrary.Editor.Translation
{
    public class Messages
    {
        private const string LanguagePrefKey = "LANGUAGE";

        public enum Language
        {
            Ja,
            En
        }

        private static Dictionary<string, string> _contents;

        private static Messages _current;

        public static Messages Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new Messages();
                }

                return _current;
            }
        }

        public Language CurrentLanguage { get; private set; }

        private Messages()
        {
            Language defaultLanguage;
            var systemLanguage = Application.systemLanguage;
            switch (systemLanguage)
            {
                case SystemLanguage.Japanese:
                    defaultLanguage = Language.Ja;
                    break;
                case SystemLanguage.English:
                    defaultLanguage = Language.En;
                    break;
                default:
                    defaultLanguage = Language.En;
                    break;
            }

            var language =
                CscEditorPrefsUtil.GetPrefs(CscEditorPrefsUtil.PrefsType.User, LanguagePrefKey, defaultLanguage);
            CurrentLanguage = language;
        }

        public string this[string key]
        {
            get
            {
                _contents ??= LoadMessageContents(CurrentLanguage);
                return _contents.GetValueOrDefault(key, key);
            }
        }

        public void ChangeLanguage(Language language)
        {
            CscEditorPrefsUtil.SetPrefs(CscEditorPrefsUtil.PrefsType.User, LanguagePrefKey, language);
            CurrentLanguage = language;
            _contents = LoadMessageContents(language);
        }

        private Dictionary<string, string> LoadMessageContents(Language language)
        {
            var contents = MessageContentsCatalog.Instance.contentsArray[(int)language];
            return contents.contents.ToDictionary(x => x.key, x => x.body);
        }

        public void Reload()
        {
            _contents = LoadMessageContents(CurrentLanguage);
        }
    }
}