using Newtonsoft.Json;

namespace Drive.Client.Models.EntityModels {
    public class DriveAutoSearch {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }
    }
}
