namespace Drive.Client.Models.Rest.EndPoints {
    public class SignalGateways {

        private static readonly string _ANNOUNCEMENTS = "/hubs/posts";

        private static readonly string _VEHICLES = "TODO";

        public SignalGateways(string host) {
            Announcements = string.Format("{0}{1}", host, _ANNOUNCEMENTS);
            Vehicles = string.Format("{0}{1}", host, _VEHICLES);
        }

        public string Announcements { get; private set; }

        public string Vehicles { get; private set; }
    }
}
