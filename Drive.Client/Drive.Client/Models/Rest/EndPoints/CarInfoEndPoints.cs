namespace Drive.Client.Models.Rest.EndPoints {
    public class CarInfoEndPoints {

        private const string GET_BY_NUMBER_API_KEY = "api/v1/auto/get?number={0}";

        private const string AUTOCOMPLETE_API_KEY = "api/v1/auto/search/numbers?value={0}";

        public CarInfoEndPoints(string defaultEndPoint) {
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

        public string GetByNumberEndPoint { get; set; }

        public string AutoCompleteEndpoint { get; set; }

        private void UpdateEndpoint(string baseEndpoint) {
            GetByNumberEndPoint = $"{baseEndpoint}/{GET_BY_NUMBER_API_KEY}";
            AutoCompleteEndpoint = $"{baseEndpoint}/{AUTOCOMPLETE_API_KEY}";
        }
    }
}
