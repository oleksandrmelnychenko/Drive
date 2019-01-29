using Drive.Client.Models.Calculator.TODO;
using Newtonsoft.Json;

namespace Drive.Client.Models.Calculator {
    public abstract class CustomsClearanceCalculatorFormBase {

        [JsonProperty("vehicleCost")]
        public int VehicleCost { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("carType")]
        public VehicleType CarType { get; set; }

        [JsonProperty("vehicleAge")]
        public int VehicleAge { get; set; }

        [JsonProperty("engineType")]
        public VehicleType EngineType { get; set; }

        [JsonProperty("engineCapacity")]
        public int EngineCapacity { get; set; }
    }
}
