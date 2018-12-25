using Newtonsoft.Json;
using System;

namespace Drive.Client.Models.EntityModels.Announcement {
    public class AnnounceBody {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("Created")]
        public DateTime Created { get; set; }

        [JsonProperty("Type")]
        public AnnounceType Type { get; set; }

        [JsonProperty("Content")]
        public string Content { get; set; }

        [JsonProperty("CommentsCount")]
        public long CommentsCount { get; set; }

        [JsonProperty("AvatarUrl")]
        public string AvatarUrl { get; set; }
        
        [JsonProperty("UserNetId")]
        public string UserNetId { get; set; }
    }
}
