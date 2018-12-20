using Newtonsoft.Json;

namespace Drive.Client.Models.EntityModels.Announcement.Comments {
    public class CommentCountBody {
        [JsonProperty("PostId")]
        public string PostId { get; set; }

        [JsonProperty("CommentsCount")]
        public int CommentsCount { get; set; }
    }
}
