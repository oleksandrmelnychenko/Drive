using Newtonsoft.Json;

namespace Drive.Client.Models.Calculator {
    public class CarCalculatorForm : CustomsClearanceCalculatorFormBase {

        [JsonProperty("gracePeriod")]
        public bool GracePeriod { get; set; }
    }
}
