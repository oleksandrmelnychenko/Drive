using Newtonsoft.Json;

namespace Drive.Client.Models.EntityModels.Search {
    public class DriveAutoSearch {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
    }
}
