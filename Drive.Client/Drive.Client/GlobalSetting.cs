using Drive.Client.Helpers;
using Drive.Client.Models.Identities;
using Drive.Client.Models.Rest;
using Newtonsoft.Json;

namespace Drive.Client {
    public class GlobalSetting {

        public UserProfile UserProfile { get; private set; }

        public RestEndpoints RestEndpoints { get; private set; } = new RestEndpoints();

        /// <summary>
        ///     ctor().
        /// </summary>
        public GlobalSetting() {
            string jsonUserProfile = Settings.UserProfile;
            UserProfile = (string.IsNullOrEmpty(jsonUserProfile)) ? new UserProfile() : JsonConvert.DeserializeObject<UserProfile>(jsonUserProfile);
        }
    }
}
