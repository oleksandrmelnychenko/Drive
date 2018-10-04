using Newtonsoft.Json;

namespace Drive.Client.Models.EntityModels.Identity {
    public class SignUpResult {
        [JsonProperty("IsSucceed")]
        public bool IsSucceed { get; set; }

        [JsonProperty("Errors")]
        public Error[] Errors { get; set; }

        [JsonProperty("AccessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("RefreshToken")]
        public string RefreshToken { get; set; }

        [JsonProperty("User")]
        public User User { get; set; }
    }
}
