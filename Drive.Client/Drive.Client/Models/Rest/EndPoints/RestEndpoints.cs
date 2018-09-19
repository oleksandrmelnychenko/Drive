using Drive.Client.Models.Rest.EndPoints;

namespace Drive.Client.Models.Rest {
    public class RestEndpoints {
        
        public const string DEFAULT_ENDPOINT = "http://31.128.79.4:13828/m";

        /// <summary>
        /// Car info endpoints
        /// </summary>
        public CarInfoEndPoints CarInfoEndPoints { get; private set; } = new CarInfoEndPoints(DEFAULT_ENDPOINT);
    }
}
