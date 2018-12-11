using Newtonsoft.Json;
using System;

namespace Drive.Client.Models.EntityModels.Announcement {
    public class Announce {

        [JsonProperty("UserNetId")]
        public string UserNetId { get; set; }

        [JsonProperty("Type")]
        public AnnounceType Type { get; set; }

        [JsonProperty("Content")]
        public string Content { get; set; } = "Hello beautifull world";

        [JsonProperty("CommentsCount")]
        public int CommentsCount { get; set; }

        [JsonProperty("NetId")]
        public string NetId { get; set; }

        /// <summary>
        /// TODO: temporary implementation
        /// </summary>
        public string AuthorAvatar { get; set; } = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT6vxI3cA6w8ocAr3KWNHdMmq5gkkhYSuacNYaFCrQxZmeBUo2m";

        /// <summary>
        /// TODO: temporary implementation
        /// </summary>
        public string MediaUrl { get; set; } = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQYk-142tJ_3kch9mdKKBHhc-hgs0VyCK02i5xcUBiCvXFRgGhj";

        /// <summary>
        /// TODO: temporary implementation
        /// </summary>
        public string AuthorName { get; set; } = "Todd Willson";

        /// <summary>
        /// TODO: temporary implementation
        /// </summary>
        public DateTime PublishDate { get; set; } = DateTime.Now;
    }

    public class TODOAnnounce {

        [JsonProperty("Type")]
        public AnnounceType Type { get; set; }

        [JsonProperty("Content")]
        public string Content { get; set; }
    }
}
