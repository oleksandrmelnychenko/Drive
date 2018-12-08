namespace Drive.Client.Models.Rest.EndPoints {
    public class SignalGateways {

        private static readonly string _ANNOUNCEMENTS = "/hubs/announces";

        public SignalGateways(string host) {
            Announcements = string.Format("{0}{1}", host, _ANNOUNCEMENTS);
        }

        public string Announcements { get; private set; }
    }
}
