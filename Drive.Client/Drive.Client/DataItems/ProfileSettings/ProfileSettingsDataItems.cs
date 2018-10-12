using Drive.Client.Models.DataItems.ProfileSettings;
using Drive.Client.Models.Identities.AppInterface.Language;
using System.Collections.Generic;

namespace Drive.Client.DataItems.ProfileSettings {
    public class ProfileSettingsDataItems : IProfileSettingsDataItems {

        private static readonly string _UKRAINIAN_LANGUAGE_TITLE = "Українська";
        private static readonly string _ENGLISH_LANGUAGE_TITLE = "English";

        public List<LanguageDataItem> BuildLanguageDataItems() =>
            new List<LanguageDataItem>() {
                new LanguageDataItem() {
                    Title = _UKRAINIAN_LANGUAGE_TITLE,
                    Language = new Ukrainian()
                },
                new LanguageDataItem() {
                    Language = new English(),
                    Title = _ENGLISH_LANGUAGE_TITLE
                }
            };


        //public List<LanguageDataItem> BuildLanguageDataItems() =>
        //    new List<LanguageDataItem>() {
        //        new LanguageDataItem() {
        //            Language = LanguageInterface.Ukrainian,
        //            Title = _UKRAINIAN_LANGUAGE_TITLE,
        //            Culture = new CultureInfo(ResourceLoader.UKRAINIAN_LOCALE_ID)
        //        },
        //        new LanguageDataItem() {
        //            Language = LanguageInterface.English,
        //            Title = _ENGLISH_LANGUAGE_TITLE,
        //            Culture = new CultureInfo(ResourceLoader.ENGLISH_LOCALE_ID)
        //        }
        //    };
    }
}
