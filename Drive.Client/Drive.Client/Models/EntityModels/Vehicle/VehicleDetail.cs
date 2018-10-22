using Newtonsoft.Json;
using System;

namespace Drive.Client.Models.EntityModels.Vehicle {
    public class VehicleDetail {
        [JsonProperty("Created")]
        public DateTime Created { get; set; }

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
