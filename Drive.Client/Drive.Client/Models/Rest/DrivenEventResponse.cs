using Newtonsoft.Json;
using System.Net;

namespace Drive.Client.Models.Rest {
    public class DrivenEventResponse : DrivenEventBase {

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("StatusCode")]
        public HttpStatusCode StatusCode { get; set; }
    }
}
