using Drive.Client.Models.EntityModels.Search.Contracts;
using Newtonsoft.Json;
using System;

namespace Drive.Client.Models.EntityModels.Search {
    public class PolandVehicleRequest : IUserVehicleRequest {
        [JsonProperty("Created")]
        public DateTime Created { get; set; }

        [JsonProperty("RequestId")]
        public Guid RequestId { get; set; }

        [JsonProperty("Vin")]
        public string Vin { get; set; }

        [JsonProperty("Date")]
        public string Date { get; set; }

        [JsonProperty("Number")]
        public string Number { get; set; }

        [JsonProperty("IsParsed")]
        public bool IsParsed { get; set; }
    }
}
