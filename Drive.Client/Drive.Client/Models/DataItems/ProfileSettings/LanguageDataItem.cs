using Drive.Client.Models.Identities.AppInterface.Language;
using System.Globalization;

namespace Drive.Client.Models.DataItems.ProfileSettings {
    public class LanguageDataItem {

        public string Title { get; set; }

        public AppLanguage Language { get; set; }

        //public LanguageInterface Language { get; set; }

        //public CultureInfo Culture { get; set; }
    }
}
