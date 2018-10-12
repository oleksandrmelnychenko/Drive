using Drive.Client.Models.DataItems.ProfileSettings;
using System.Globalization;
using System.Runtime.Serialization;

namespace Drive.Client.Models.Identities.AppInterface.Language {
    [DataContract]
    public abstract class AppLanguage {

        [DataMember]
        public string LocaleId { get; protected set; }

        [DataMember]
        public LanguageInterface LanguageInterface { get; protected set; }

        [DataMember]
        public CultureInfo Culture { get; protected set; }
    }
}