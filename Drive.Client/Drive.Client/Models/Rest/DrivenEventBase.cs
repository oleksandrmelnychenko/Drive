using Newtonsoft.Json;

namespace Drive.Client.Models.Rest {
    public abstract class DrivenEventBase {

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Data")]
        public object Data { get; set; }
    }
}
