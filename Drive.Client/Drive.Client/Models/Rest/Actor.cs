using Newtonsoft.Json;

namespace Drive.Client.Models.Rest {
    public class Actor {

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("EventType")]
        public int EventType { get; set; }

        [JsonProperty("UserNetId")]
        public string UserNetId { get; set; }

        [JsonProperty("Data")]
        public object Data { get; set; }
    }
}
