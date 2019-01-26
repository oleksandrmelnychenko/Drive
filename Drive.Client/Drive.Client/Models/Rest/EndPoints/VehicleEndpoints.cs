namespace Drive.Client.Models.Rest.EndPoints {
    public class VehicleEndpoints {

        private const string USER_VEHICLE_DETAIL_REQUESTS_API_KEY = "m/api/v1/resident/vehicle/details/get/all";

        private const string GET_VEHICLE_DETAILS_API_KEY = "m/api/v1/resident/vehicle/details/get?requestId={0}";

        private const string GET_VEHICLE_DETAILS_BY_RESIDENT_FULLNAME_API_KEY = "m/api/v1/resident/vehicle/details/new?first={0}&last={1}&middle={2}&birth={3}";

        private const string GET_NEW_POLAND_VEHICLE_DETAILS_API_KEY = "m/api/v1/auto/get/poland?vin={0}&date={1}&number={2}";

        private const string GET_POLAND_VEHICLE_REQUESTS_API_KEY = "m/api/v1/auto/get/poland/user/requests";

        private const string GET_POLAND_VEHICLE_DETAILS_BY_REQUESTID_API_KEY = "m/api/v1/auto/get/poland/request?requestId={0}";

        private const string GET_COGNITIVE_REQUESTS_API_KEY = "m/api/v1/resident/vehicle/details/get/cognitive";

        /// <summary>
        ///     ctor().
        /// </summary>
        /// <param name="defaultEndPoint"></param>
        public VehicleEndpoints(string defaultEndPoint) {
            BaseEndpoint = defaultEndPoint;
        }

        string _baseEndpoint;
        public string BaseEndpoint {
            get { return _baseEndpoint; }
            set {
                _baseEndpoint = value;
                UpdateEndpoint(_baseEndpoint);
            }
        }

        public string UserVehicleDetailRequestsEndpoint { get; private set; }

        public string VehicleDetailsEndpoint { get; private set; }

        public string VehicleDetailsByResidentFullNameEndpoint { get; private set; }

        public string PolandVehicleDetailsEndpoint { get; private set; }

        public string PolandVehicleRequestsEndpoint { get; private set; }

        public string CognitiveRequestsEndpoint { get; private set; }

        public string PolandVehicleDetailsByRequestIdEndpoint { get; private set; }

        private void UpdateEndpoint(string baseEndpoint) {
            UserVehicleDetailRequestsEndpoint = $"{baseEndpoint}/{USER_VEHICLE_DETAIL_REQUESTS_API_KEY}";
            VehicleDetailsEndpoint = $"{baseEndpoint}/{GET_VEHICLE_DETAILS_API_KEY}";
            VehicleDetailsByResidentFullNameEndpoint = $"{baseEndpoint}/{GET_VEHICLE_DETAILS_BY_RESIDENT_FULLNAME_API_KEY}";
            PolandVehicleDetailsEndpoint = $"{baseEndpoint}/{GET_NEW_POLAND_VEHICLE_DETAILS_API_KEY}";
            PolandVehicleRequestsEndpoint = $"{baseEndpoint}/{GET_POLAND_VEHICLE_REQUESTS_API_KEY}";
            PolandVehicleDetailsByRequestIdEndpoint = $"{baseEndpoint}/{GET_POLAND_VEHICLE_DETAILS_BY_REQUESTID_API_KEY}";
            CognitiveRequestsEndpoint = $"{baseEndpoint}/{GET_COGNITIVE_REQUESTS_API_KEY}";
        }
    }
}
