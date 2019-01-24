namespace Drive.Client.Models.Rest.EndPoints {
    public class CarInfoEndPoints {

        private const string GET_BY_NUMBER_API_KEY = "m/api/v1/auto/get?number={0}";

        private const string AUTOCOMPLETE_API_KEY = "m/api/v1/auto/search/numbers?value={0}";

        private const string GET_ALL_API_KEY = "m/api/v1/auto/get/all?number={0}";

        private const string SEARCH_BY_COGNITIVE_API_KEY = "m/api/v1/auto/parse/cognitive/image/data";

        /// <summary>
        ///     ctor().
        /// </summary>
        /// <param name="defaultEndPoint"></param>
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

        public string GetByNumberEndPoint { get; private set; }

        public string AutoCompleteEndpoint { get; private set; }

        public string GetAllCarInfoEndPoint { get; private set; }

        public string SearchByCognitiveEndPoint { get; private set; }

        private void UpdateEndpoint(string baseEndpoint) {
            GetByNumberEndPoint = $"{baseEndpoint}/{GET_BY_NUMBER_API_KEY}";
            AutoCompleteEndpoint = $"{baseEndpoint}/{AUTOCOMPLETE_API_KEY}";
            GetAllCarInfoEndPoint = $"{baseEndpoint}/{GET_ALL_API_KEY}";
            SearchByCognitiveEndPoint = $"{baseEndpoint}/{SEARCH_BY_COGNITIVE_API_KEY}";
        }
    }
}
