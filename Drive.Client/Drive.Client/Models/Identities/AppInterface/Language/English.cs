using Drive.Client.Helpers.Localize;
using System.Globalization;
using System.Runtime.Serialization;

namespace Drive.Client.Models.Identities.AppInterface.Language {

    [DataContract]
    public class English : AppLanguage {
        public English() {
            Culture = new CultureInfo(ResourceLoader.ENGLISH_LOCALE_ID);
            LocaleId = ResourceLoader.ENGLISH_LOCALE_ID;
            LanguageInterface = LanguageInterface.English;
        }
    }
}
