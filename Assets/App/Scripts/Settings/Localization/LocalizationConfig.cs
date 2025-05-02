using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace App.Settings.Localization
{
    [CreateAssetMenu(fileName = nameof(LocalizationConfig), menuName = Consts.ConfigsPath + "Settings/" + nameof(LocalizationConfig))]
    public class LocalizationConfig : SettingsConfig
    {
        public static string GetSystemLocaleCode()
        {
            var localeCode = "en";
            if (Application.systemLanguage == SystemLanguage.Russian) 
                localeCode = "ru";

            return localeCode;
        }

        public static Locale GetSystemLocale()
        {
            var localeCode = GetSystemLocaleCode();
            var locales = LocalizationSettings.AvailableLocales.Locales;

            var systemLocale = locales.FirstOrDefault(v => v.Identifier.Code == localeCode);
            return systemLocale;
        }

        public static int GetLocaleIndex(Locale locale) 
            => LocalizationSettings.AvailableLocales.Locales.IndexOf(locale);

        public static IReadOnlyList<Locale> GetLocales() 
            => LocalizationSettings.AvailableLocales.Locales;
    }
}