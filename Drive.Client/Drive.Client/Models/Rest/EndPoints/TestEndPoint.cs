namespace Drive.Client.Models.Rest.EndPoints {
    public class TestEndPoint {

        private const string TEST_API_KEY = "api/v1/test?test={0}";

        public TestEndPoint(string defaultEndPoint) {
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

        public string TestRequest { get; set; }

        private void UpdateEndpoint(string baseEndpoint) {
            TestRequest = $"{baseEndpoint}/{TEST_API_KEY}";
        }
    }
}
