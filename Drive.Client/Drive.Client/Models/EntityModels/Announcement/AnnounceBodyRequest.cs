using Newtonsoft.Json;

namespace Drive.Client.Models.EntityModels.Announcement {
    public class AnnounceBodyRequest {
        [JsonProperty("Type")]
        public AnnounceType Type { get; set; }

        [JsonProperty("Content")]
        public string Content { get; set; }
    }
}
