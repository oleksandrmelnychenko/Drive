using Newtonsoft.Json;

namespace Drive.Client.Models.EntityModels.Announcement {
    public class Announce {
        [JsonProperty("ImageUrl")]
        public string[] ImageUrl { get; set; }

        [JsonProperty("IsLikedByUser")]
        public bool IsLikedByUser { get; set; }

        [JsonProperty("XamarinPost")]
        public AnnounceBody AnnounceBody { get; set; }
    }
}
