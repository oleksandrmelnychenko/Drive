using Newtonsoft.Json;
using System;

namespace Drive.Client.Models.EntityModels.Search {
    public class ResidentRequest {
        [JsonProperty("Created")]
        public DateTime Created { get; set; }

        [JsonProperty("ResidentFirstName")]
        public string ResidentFirstName { get; set; }

        [JsonProperty("ResidentLastName")]
        public string ResidentLastName { get; set; }

        [JsonProperty("ResidentMiddleName")]
        public string ResidentMiddleName { get; set; }

        [JsonProperty("ResidentDateOfBirth")]
        public DateTime? ResidentDateOfBirth { get; set; }

        [JsonProperty("Status")]
        public Status Status { get; set; }

        [JsonProperty("GovRequestId")]
        public long GovRequestId { get; set; }

        [JsonProperty("VechicalCount")]
        public long VechicalCount { get; set; }
    }
}
