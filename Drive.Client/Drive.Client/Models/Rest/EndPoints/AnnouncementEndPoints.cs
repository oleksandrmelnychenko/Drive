namespace Drive.Client.Models.Rest.EndPoints {
    public class AnnouncementEndPoints {

        private static readonly string _NEW_ANNOUNCE = "/m/api/v1/event/sourcing/new";

        private const string UPLOAD_ATTACHED_DATA_API_KEY = "/m/api/v1/event/sourcing/new/formData?EventId={0}&EventType={1}";

        public string NewAnnounce { get; private set; }

        public string UploadAttachedDataEndpoint { get; internal set; }

        public AnnouncementEndPoints(string host) {
            NewAnnounce = string.Format("{0}{1}", host, _NEW_ANNOUNCE);

            UploadAttachedDataEndpoint = $"{host}/{UPLOAD_ATTACHED_DATA_API_KEY}";
        }
    }
}
