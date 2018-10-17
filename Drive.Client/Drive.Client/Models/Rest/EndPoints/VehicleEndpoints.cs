namespace Drive.Client.Models.Rest.EndPoints {
    public class VehicleEndpoints {

        private const string USER_VEHICLE_DETAIL_REQUESTS_API_KEY = "api/v1/resident/vechical/details/get/all";

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

        public string UserVehicleDetailRequests { get; private set; }

        private void UpdateEndpoint(string baseEndpoint) {
            UserVehicleDetailRequests = $"{baseEndpoint}/{USER_VEHICLE_DETAIL_REQUESTS_API_KEY}";
        }
    }
}
