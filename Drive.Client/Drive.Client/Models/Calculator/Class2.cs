using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.Models.Calculator {
    class Class2 {
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

        [JsonProperty("trackWeight")]
        public long TrackWeight { get; set; }
    }
}
