using Drive.Client.Models.DataItems.ProfileSettings;
using System.Collections.Generic;

namespace Drive.Client.DataItems.ProfileSettings {
    public class ProfileSettingsDataItems : IProfileSettingsDataItems {

        private static readonly string _UKRAINIAN_LANGUAGE_TITLE = "Українська";
        private static readonly string _ENGLISH_LANGUAGE_TITLE = "English";

        public List<LanguageDataItem> BuildLanguageDataItems() =>
            new List<LanguageDataItem>() {
                new LanguageDataItem() {
                    Language = LanguageInterface.Ukrainian,
                    Title = _UKRAINIAN_LANGUAGE_TITLE
                },
                new LanguageDataItem() {
                    Language = LanguageInterface.English,
                    Title = _ENGLISH_LANGUAGE_TITLE
                }
            };
    }
}
