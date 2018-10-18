namespace Drive.Client.Models.Rest.EndPoints {
    public class VehicleEndpoints {

        private const string USER_VEHICLE_DETAIL_REQUESTS_API_KEY = "api/v1/resident/vehicle/details/get/all";

        private const string GET_VEHICLE_DETAILS_API_KEY = "api/v1/resident/vehicle/details/get?requestId={0}";

        private const string GET_VEHICLE_DETAILS_BY_RESIDENT_FULLNAME_API_KEY = "api/v1/resident/vehicle/details/new?first={0}&last={1}&middle={2}&birth={3}";

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

        private void UpdateEndpoint(string baseEndpoint) {
            UserVehicleDetailRequestsEndpoint = $"{baseEndpoint}/{USER_VEHICLE_DETAIL_REQUESTS_API_KEY}";
            VehicleDetailsEndpoint = $"{baseEndpoint}/{GET_VEHICLE_DETAILS_API_KEY}";
            VehicleDetailsByResidentFullNameEndpoint = $"{baseEndpoint}/{GET_VEHICLE_DETAILS_BY_RESIDENT_FULLNAME_API_KEY}";
        }
    }
}
