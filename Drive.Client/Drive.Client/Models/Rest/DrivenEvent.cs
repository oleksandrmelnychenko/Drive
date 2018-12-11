using Newtonsoft.Json;

namespace Drive.Client.Models.Rest {
    public class DrivenEvent : DrivenEventBase {

        [JsonProperty("EventType")]
        public DrivenActorEvents EventType { get; set; }

        [JsonProperty("UserNetId")]
        public string UserNetId { get; set; }
    }
}
