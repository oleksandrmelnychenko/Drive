using Newtonsoft.Json;

namespace Drive.Client.Models.EntityModels.Search {
    public class ResidentName {
        [JsonProperty("Case")]
        public string Case { get; set; }

        [JsonProperty("Fields")]
        public string[] Fields { get; set; }
    }
}
