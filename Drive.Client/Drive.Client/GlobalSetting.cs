using Drive.Client.Helpers;
using Drive.Client.Helpers.AppEvents;
using Drive.Client.Models.Identities;
using Drive.Client.Models.Rest;
using Drive.Client.ViewModels.Base;
using Newtonsoft.Json;

namespace Drive.Client {
    public class GlobalSetting {

        public string MessagingDeviceToken { get; set; }

        public UserProfile UserProfile { get; private set; }

        public AppMessagingEvents AppMessagingEvents => DependencyLocator.Resolve<AppMessagingEvents>();

        public AppInterfaceConfigurations AppInterfaceConfigurations { get; private set; }

        public RestEndpoints RestEndpoints { get; private set; } = new RestEndpoints();

        /// <summary>
        ///     ctor().
        /// </summary>
        public GlobalSetting() {
            string jsonUserProfile = Settings.UserProfile;
            UserProfile = (string.IsNullOrEmpty(jsonUserProfile)) ? new UserProfile() : JsonConvert.DeserializeObject<UserProfile>(jsonUserProfile);

            string appInterfaceConfigurations = Settings.AppInterfaceConfigurations;
            AppInterfaceConfigurations = (string.IsNullOrEmpty(appInterfaceConfigurations)) ? new AppInterfaceConfigurations() : JsonConvert.DeserializeObject<AppInterfaceConfigurations>(appInterfaceConfigurations);
        }

        public void SaveState() {
            Settings.UserProfile = JsonConvert.SerializeObject(BaseSingleton<GlobalSetting>.Instance.UserProfile);
            Settings.AppInterfaceConfigurations = JsonConvert.SerializeObject(BaseSingleton<GlobalSetting>.Instance.AppInterfaceConfigurations);
        }
    }
}
