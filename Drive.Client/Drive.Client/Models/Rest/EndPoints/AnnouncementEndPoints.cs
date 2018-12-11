namespace Drive.Client.Models.Rest.EndPoints {
    public class AnnouncementEndPoints {

        private static readonly string _NEW_ANNOUNCE = "/m/api/v1/event/sourcing/new";

        public AnnouncementEndPoints(string host) {
            NewAnnounce = string.Format("{0}{1}", host, _NEW_ANNOUNCE);
        }

        public string NewAnnounce { get; private set; }
    }
}
