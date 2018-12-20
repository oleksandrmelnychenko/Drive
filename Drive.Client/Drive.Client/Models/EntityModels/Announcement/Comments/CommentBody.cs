using Newtonsoft.Json;

namespace Drive.Client.Models.EntityModels.Announcement.Comments {
    public class CommentBody {
        [JsonProperty("PostId")]
        public string PostId { get; set; }

        [JsonProperty("PostCommentId")]
        public string PostCommentId { get; set; }

        [JsonProperty("TextContent")]
        public string TextContent { get; set; }       
    }
}
