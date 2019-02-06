using Newtonsoft.Json;

namespace Drive.Client.Models.Calculator {
    public class ExchangeRate {
        [JsonProperty("ccy")]
        public string Ccy { get; set; }

        [JsonProperty("base_ccy")]
        public string BaseCcy { get; set; }

        [JsonProperty("buy")]
        public string Buy { get; set; }

        [JsonProperty("sale")]
        public string Sale { get; set; }
    }
}
