using System.Collections.Generic;
using ChickStar.CommonLibrary.Editor.Common.DrawUtils.Drawer;
using ChickStar.CommonLibrary.Editor.Translation;
using UnityEditor;
using UnityEngine;

namespace ChickStar.CommonLibrary.Editor
{
    public class CsCommonLibrarySettingsProvider : SettingsProvider
    {
        private const string SettingPath = "Preferences/ChickStar/CommonLibrary Preference";
        private readonly EnumPopupDrawer<Messages.Language> _languagePopupDrawer;

        private CsCommonLibrarySettingsProvider(string path, SettingsScope scopes,
            IEnumerable<string> keywords = null) :
            base(path, scopes, keywords)
        {
            _languagePopupDrawer = new EnumPopupDrawer<Messages.Language>(
                defaultIndex: (int)Messages.Current.CurrentLanguage,
                displayOverrideOption: new Dictionary<Messages.Language, string>()
                {
                    { Messages.Language.Ja, "日本語" },
                    { Messages.Language.En, "English" }
                }
            );
        }

        [SettingsProvider]
        public static SettingsProvider CreateSettingProvider()
        {
            return new CsCommonLibrarySettingsProvider(SettingPath, SettingsScope.User);
        }

        public override void OnGUI(string searchContext)
        {
            DrawLanguageSettings();
        }

        private void DrawLanguageSettings()
        {
            var selectedLanguage = (Messages.Language)_languagePopupDrawer.Draw(
                Messages.Current["Settings.Language"]
            );

            if (selectedLanguage != Messages.Current.CurrentLanguage)
            {
                Messages.Current.ChangeLanguage(selectedLanguage);
            }
        }
    }
}