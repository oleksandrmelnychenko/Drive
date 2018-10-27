using Newtonsoft.Json;

namespace Drive.Client.Models.EntityModels.Search {
    public class PolandVehicleDetail {
        [JsonProperty("Brand")]
        public string Brand { get; set; }

        [JsonProperty("Model")]
        public string Model { get; set; }

        [JsonProperty("Year")]
        public string Year { get; set; }

        [JsonProperty("VIN")]
        public string VIN { get; set; }

        [JsonProperty("VehicleTechnicalInspection")]
        public string VehicleTechnicalInspection { get; set; }

        [JsonProperty("LastOdometrData")]
        public string LastOdometrData { get; set; }

        [JsonProperty("RegistrationStatus")]
        public string RegistrationStatus { get; set; }

        [JsonProperty("EngineCapacity")]
        public string EngineCapacity { get; set; }

        [JsonProperty("EnginePower")]
        public string EnginePower { get; set; }

        [JsonProperty("FuelType")]
        public string FuelType { get; set; }

        [JsonProperty("TotalCapacity")]
        public string TotalCapacity { get; set; }

        [JsonProperty("NumberOfSeats")]
        public string NumberOfSeats { get; set; }

        [JsonProperty("CurbWeight")]
        public string CurbWeight { get; set; }

        [JsonProperty("MaximumLadenMassOfBrakedTrailer")]
        public string MaximumLadenMassOfBrakedTrailer { get; set; }

        [JsonProperty("MaximumLadenMassOfUnbrakedTrailer")]
        public string MaximumLadenMassOfUnbrakedTrailer { get; set; }

        [JsonProperty("MaximumPermissibleTowableMass")]
        public string MaximumPermissibleTowableMass { get; set; }

        [JsonProperty("NumberOfAxles")]
        public string NumberOfAxles { get; set; }

        [JsonProperty("DateСurrentVehicleRegistrationCertificateIssued")]
        public string DateСurrentVehicleRegistrationCertificateIssued { get; set; }

        [JsonProperty("DateVehicleRecordDocumentIssued")]
        public string DateVehicleRecordDocumentIssued { get; set; }

        [JsonProperty("CivilLiabilityInsurance")]
        public string CivilLiabilityInsurance { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }
    }
}
