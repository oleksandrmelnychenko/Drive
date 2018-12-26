using Newtonsoft.Json;

namespace Drive.Client.Models.EntityModels.Announcement {
    public class PostLikedBody {
        [JsonProperty("PostId")]
        public string PostId { get; set; }

        [JsonProperty("LikesCount")]
        public long LikesCount { get; set; }

        [JsonProperty("PostLikedByUser")]
        public PostLikedByUser PostLikedByUser { get; set; }
    }
}
