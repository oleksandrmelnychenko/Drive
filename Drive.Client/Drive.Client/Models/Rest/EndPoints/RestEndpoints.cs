using Drive.Client.Models.Rest.EndPoints;

namespace Drive.Client.Models.Rest {
    public class RestEndpoints {

        public const string DEFAULT_ENDPOINT = "http://31.128.79.4:13828"; // Release
        //public const string DEFAULT_ENDPOINT = "http://31.128.79.4:13829"; // Development

        /// <summary>
        /// Car info endpoints.
        /// </summary>
        public CarInfoEndPoints CarInfoEndPoints { get; private set; } = new CarInfoEndPoints(DEFAULT_ENDPOINT);
        /// <summary>
        /// Client hardware endpoints.
        /// </summary>
        public ClientHardwareEndPoints ClientHardwareEndPoints { get; private set; } = new ClientHardwareEndPoints(DEFAULT_ENDPOINT);
        /// <summary>
        /// Identity endpoints.
        /// </summary>
        public IdentityEndpoints IdentityEndpoints { get; set; } = new IdentityEndpoints(DEFAULT_ENDPOINT);
        /// <summary>
        /// Vehicle endpoints
        /// </summary>
        public VehicleEndpoints VehicleEndpoints { get; set; } = new VehicleEndpoints(DEFAULT_ENDPOINT);

        public SignalGateways SignalGateways { get; set; } = new SignalGateways(DEFAULT_ENDPOINT);
    }
}
