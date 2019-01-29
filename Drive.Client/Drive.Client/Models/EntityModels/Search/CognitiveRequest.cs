using Drive.Client.Models.EntityModels.Search.Contracts;
using Newtonsoft.Json;
using System;

namespace Drive.Client.Models.EntityModels.Search {
    public class CognitiveRequest : IUserVehicleRequest {
        [JsonProperty("NetId")]
        public string NetId { get; set; }

        [JsonProperty("ImageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("Created")]
        public DateTime Created { get; set; }

        [JsonProperty("VehicleNumber")]
        public string VehicleNumber { get; set; }

        [JsonProperty("HistoryType")]
        public RequestType HistoryType { get; set; }
    }
}
