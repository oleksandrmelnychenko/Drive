using Newtonsoft.Json;
using System;

namespace Drive.Client.Models.EntityModels.Search {
    public class VehicleDetailsByResidentFullName {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("ResidentName")]
        public ResidentName ResidentName { get; set; }

        [JsonProperty("Status")]
        public Status Status { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Vehicles")]
        public object[] Vehicles { get; set; }

        [JsonProperty("Created")]
        public DateTime Created { get; set; }
    }
}
