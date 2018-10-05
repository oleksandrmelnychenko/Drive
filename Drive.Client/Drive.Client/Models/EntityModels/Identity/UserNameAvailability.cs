using Newtonsoft.Json;

namespace Drive.Client.Models.EntityModels.Identity {
    public class UserNameAvailability {
        [JsonProperty("isRequestSuccess")]
        public bool IsRequestSuccess { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public bool Data { get; set; }
    }
}
