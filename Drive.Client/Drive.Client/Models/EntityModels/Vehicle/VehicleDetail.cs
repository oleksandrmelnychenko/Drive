using Newtonsoft.Json;

namespace Drive.Client.Models.EntityModels.Vehicle {
    public class VehicleDetail {
        [JsonProperty("Brand")]
        public string Brand { get; set; }

        [JsonProperty("Model")]
        public string Model { get; set; }

        [JsonProperty("Year")]
        public string Year { get; set; }

        [JsonProperty("Data")]
        public string Data { get; set; }

        [JsonProperty("Volume")]
        public string Volume { get; set; }
    }
}
