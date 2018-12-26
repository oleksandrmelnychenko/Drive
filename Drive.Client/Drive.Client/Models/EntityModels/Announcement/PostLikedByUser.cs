using Newtonsoft.Json;
using System;

namespace Drive.Client.Models.EntityModels.Announcement {
    public class PostLikedByUser {
        [JsonProperty("PostId")]
        public string PostId { get; set; }

        [JsonProperty("UserNetId")]
        public string UserNetId { get; set; }

        [JsonProperty("IsLikedByUser")]
        public bool IsLikedByUser { get; set; }
    }
}
