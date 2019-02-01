using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.Models.Calculator {
    public class CarCustoms {
        [JsonProperty("price")]
        public long Price { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("carType")]
        public string CarType { get; set; }

        [JsonProperty("year")]
        public long Year { get; set; }

        [JsonProperty("engineType")]
        public string EngineType { get; set; }

        [JsonProperty("engineCap")]
        public long EngineCap { get; set; }

        [JsonProperty("preferentialExcise")]
        public bool PreferentialExcise { get; set; }
    }
}
