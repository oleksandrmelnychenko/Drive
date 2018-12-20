using Newtonsoft.Json;
using System;

namespace Drive.Client.Models.EntityModels.Announcement.Comments {
    public class Comment {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("AvatarUrl")]
        public string AvatarUrl { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("Created")]
        public DateTime Created { get; set; }

        [JsonProperty("TextContent")]
        public string TextContent { get; set; }
    }
}
