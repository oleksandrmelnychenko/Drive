using Drive.Client.Models.Rest.EndPoints;

namespace Drive.Client.Models.Rest {
    public class RestEndpoints {
        // valid
        //public const string DEFAULT_ENDPOINT = "http://31.128.79.4:13828";
        // test
        public const string DEFAULT_ENDPOINT = "http://31.128.79.4:13823";

        /// <summary>
        /// Test api endpoints
        /// </summary>
        public TestEndPoint TestEndPoint { get; private set; } = new TestEndPoint(DEFAULT_ENDPOINT);
    }
}
