using Newtonsoft.Json;

namespace Drive.Client.Models.Calculator {
    public class TruckCalculatorForm : CustomsClearanceCalculatorFormBase {

        [JsonProperty("fullMass")]
        public int FullMass { get; set; }
    }
}
