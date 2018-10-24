using Newtonsoft.Json;
using System;

namespace Drive.Client.Models.EntityModels.Search {
    public class DriveAuto {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("person")]
        public string Person { get; set; }

        [JsonProperty("regAddrKoatuu")]
        public string RegAddrKoatuu { get; set; }

        [JsonProperty("operCode")]
        public string OperCode { get; set; }

        [JsonProperty("operName")]
        public string OperName { get; set; }

        [JsonProperty("dReg")]
        public DateTime DReg { get; set; }

        [JsonProperty("depCode")]
        public string DepCode { get; set; }

        [JsonProperty("dep")]
        public string Dep { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("modelName")]
        public string ModelName { get; set; }

        [JsonProperty("makeYear")]
        public string MakeYear { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("purpose")]
        public string Purpose { get; set; }

        [JsonProperty("fuel")]
        public string Fuel { get; set; }

        [JsonProperty("capacity")]
        public string Capacity { get; set; }

        [JsonProperty("ownWeight")]
        public string OwnWeight { get; set; }

        [JsonProperty("totalWeight")]
        public string TotalWeight { get; set; }

        [JsonProperty("nRegNew")]
        public string NRegNew { get; set; }
    }
}
