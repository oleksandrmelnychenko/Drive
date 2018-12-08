namespace Drive.Client.Models.Rest.EndPoints {
    public class ClientHardwareEndPoints {

        private static readonly string _REGISTER_CLIENT_DEVICE_GATEWAY = "/m/api/v1/client/hardware/set";

        public ClientHardwareEndPoints(string host) {
            Host = host;

            InitEndpoints(host);
        }

        public string Host { get; private set; }

        public string RegisterClientDevice { get; private set; }

        private void InitEndpoints(string host) {
            RegisterClientDevice = string.Format("{0}{1}", host, _REGISTER_CLIENT_DEVICE_GATEWAY);
        }
    }
}
