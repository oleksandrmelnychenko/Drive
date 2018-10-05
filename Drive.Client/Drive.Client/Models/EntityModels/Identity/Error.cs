using Newtonsoft.Json;

namespace Drive.Client.Models.EntityModels.Identity {
    public class Error {
        [JsonProperty("Item1")]
        public string Item1 { get; set; }

        [JsonProperty("Item2")]
        public string Item2 { get; set; }
    }
}
