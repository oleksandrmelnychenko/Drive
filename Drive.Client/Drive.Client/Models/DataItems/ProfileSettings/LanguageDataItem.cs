using System.Globalization;

namespace Drive.Client.Models.DataItems.ProfileSettings {
    public class LanguageDataItem {

        public string Title { get; set; }

        public LanguageInterface Language { get; set; }

        public CultureInfo Culture { get; set; }
    }
}
