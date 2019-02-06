using Drive.Client.Models.Calculator.TODO;
using Newtonsoft.Json;

namespace Drive.Client.Models.Calculator {
    public class CarCustoms {
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        [JsonProperty("carType")]
        public string CarType { get; set; }

        [JsonProperty("year")]
        public long Year { get; set; }

        [JsonProperty("engineType")]
        public string EngineType { get; set; }

        [JsonProperty("engineCap")]
        public double EngineCap { get; set; }

        [JsonProperty("preferentialExcise")]
        public bool PreferentialExcise { get; set; }
    }
}
