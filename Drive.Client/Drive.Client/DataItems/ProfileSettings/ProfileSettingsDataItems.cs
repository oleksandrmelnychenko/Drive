using Drive.Client.Models.DataItems.ProfileSettings;
using System.Collections.Generic;
using System.Globalization;

namespace Drive.Client.DataItems.ProfileSettings {
    public class ProfileSettingsDataItems : IProfileSettingsDataItems {

        private static readonly string _UKRAINIAN_LOCALE_ID = "uk-UA";
        private static readonly string _ENGLISH_LOCALE_ID = "en-US";

        private static readonly string _UKRAINIAN_LANGUAGE_TITLE = "Українська";
        private static readonly string _ENGLISH_LANGUAGE_TITLE = "English";

        public List<LanguageDataItem> BuildLanguageDataItems() =>
            new List<LanguageDataItem>() {
                new LanguageDataItem() {
                    Language = LanguageInterface.Ukrainian,
                    Title = _UKRAINIAN_LANGUAGE_TITLE,
                    Culture = new CultureInfo(_UKRAINIAN_LOCALE_ID)
                },
                new LanguageDataItem() {
                    Language = LanguageInterface.English,
                    Title = _ENGLISH_LANGUAGE_TITLE,
                    Culture = new CultureInfo(_ENGLISH_LOCALE_ID)
                }
            };
    }
}
