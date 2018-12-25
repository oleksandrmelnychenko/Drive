namespace Drive.Client.Models.Rest.EndPoints {
    public class AnnouncementEndPoints {

        private const string NEW_ANNOUNCE = "/m/api/v1/event/sourcing/new";

        private const string UPLOAD_ATTACHED_DATA_API_KEY = "m/api/v1/event/sourcing/new/formData?EventId={0}&EventType={1}";

        private const string GET_ANNOUNCES_API_KEY = "m/api/v1/posts/get";

        private const string GET_POST_COMMENTS_API_KEY = "m/api/v1/posts/comments/get?postId={0}";

        public string NewAnnounce { get; private set; }

        public string UploadAttachedDataEndpoint { get; internal set; }

        public string GetAnnouncesEndpoint { get; internal set; }

        public string GetPostCommentsEndpoint { get; internal set; }

        /// <summary>
        ///     ctor().
        /// </summary>
        /// <param name="host"></param>
        public AnnouncementEndPoints(string host) {
            NewAnnounce = string.Format("{0}{1}", host, NEW_ANNOUNCE);

            UploadAttachedDataEndpoint = $"{host}/{UPLOAD_ATTACHED_DATA_API_KEY}";

            GetAnnouncesEndpoint = $"{host}/{GET_ANNOUNCES_API_KEY}";

            GetPostCommentsEndpoint = $"{host}/{GET_POST_COMMENTS_API_KEY}";
        }
    }
}
