using Drive.Client.Helpers.Localize;
using System.Globalization;
using System.Runtime.Serialization;

namespace Drive.Client.Models.Identities.AppInterface.Language {

    [DataContract]
    public class Ukrainian : AppLanguage {
        public Ukrainian() {
            Culture = new CultureInfo(ResourceLoader.UKRAINIAN_LOCALE_ID);
            LocaleId = ResourceLoader.UKRAINIAN_LOCALE_ID;
            LanguageInterface = LanguageInterface.Ukrainian;
        }
    }
}
