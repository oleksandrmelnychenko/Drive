using Newtonsoft.Json;

namespace Drive.Client.Models.Calculator {
    public class CustomsResult {
        [JsonProperty("importDuty")]
        public string ImportDuty { get; set; }

        [JsonProperty("exciseDuty")]
        public string ExciseDuty { get; set; }

        [JsonProperty("VAT")]
        public string Vat { get; set; }

        [JsonProperty("bondedCarCost")]
        public string BondedCarCost { get; set; }

        [JsonProperty("customsClearanceCosts")]
        public string CustomsClearanceCosts { get; set; }

        [JsonProperty("clearedCarsCost")]
        public string ClearedCarsCost { get; set; }
    }
}
