using Drive.Client.Models.DataItems.ProfileSettings;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Drive.Client.Helpers {

    [DataContract]
    public class AppInterfaceConfigurations {

        [DataMember]
        public LanguageInterface LanguageInterface { get; set; }

        public void ClearUserProfile() {
            LanguageInterface = LanguageInterface.Ukrainian;
        }

        public void SaveChanges() {
            Settings.UserProfile = JsonConvert.SerializeObject(this);
        }
    }
}
