using Drive.Client.Models.DataItems.ProfileSettings;
using Drive.Client.Models.Identities.AppInterface.Language;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Drive.Client.Helpers {

    [DataContract]
    public class AppInterfaceConfigurations {

        [DataMember]
        public AppLanguage LanguageInterface { get; set; } = new Ukrainian();

        public void ClearUserProfile() {
            LanguageInterface = new Ukrainian();
        }

        public void SaveChanges() {
            Settings.UserProfile = JsonConvert.SerializeObject(this);
        }
    }
}
