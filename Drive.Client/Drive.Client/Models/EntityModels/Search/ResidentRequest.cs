using Newtonsoft.Json;
using System;

namespace Drive.Client.Models.EntityModels.Search {
    public class ResidentRequest {
        [JsonProperty("Created")]
        public DateTime Created { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("MiddleName")]
        public string MiddleName { get; set; }

        [JsonProperty("DateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        [JsonProperty("Status")]
        public Status Status { get; set; }

        [JsonProperty("GovRequestId")]
        public long GovRequestId { get; set; }

        [JsonProperty("VehicleCount")]
        public long VehicleCount { get; set; }
    }
}
