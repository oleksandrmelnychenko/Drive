using Newtonsoft.Json;

namespace Drive.Client.Models.EntityModels.Identity {
    public class LogOutResult  {
        [JsonProperty("IsRequestSuccess")]
        public bool IsRequestSuccess { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Data")]
        public string Data { get; set; }
    }
}
