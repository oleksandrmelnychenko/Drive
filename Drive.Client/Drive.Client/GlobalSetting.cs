using Drive.Client.Helpers;
using Drive.Client.Models.Identities;
using Drive.Client.Models.Rest;
using Newtonsoft.Json;

namespace Drive.Client {
    public class GlobalSetting {

        //public const string DEFAULT_ENDPOINT = "http://31.128.79.4:13828";

        /// <summary>
        ///     ctor().
        /// </summary>
        public GlobalSetting() {
            string jsonUserProfile = Settings.UserProfile;
            UserProfile = (string.IsNullOrEmpty(jsonUserProfile)) ? new UserProfile() : JsonConvert.DeserializeObject<UserProfile>(jsonUserProfile);
        }

        public UserProfile UserProfile { get; private set; }

        public RestEndpoints RestEndpoints { get; private set; } = new RestEndpoints();
    }
}
