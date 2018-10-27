using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.Models.Identities.Posts {
    public abstract class PostBase {

        /// <summary>
        /// Type of current post (text, media, link)
        /// </summary>
        public PostTypes PostType { get; protected set; }

        /// <summary>
        /// todo
        /// </summary>
        public string PostMessage { get; set; } = "Hello beautifull world";

        /// <summary>
        /// todo
        /// </summary>
        public string AuthorAvatar { get; set; } = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT6vxI3cA6w8ocAr3KWNHdMmq5gkkhYSuacNYaFCrQxZmeBUo2m";

        /// <summary>
        /// todo
        /// </summary>
        public string AuthorName { get; set; } = "Todd Willson";

        /// <summary>
        /// todo
        /// </summary>
        public DateTime PublishDate { get; set; } = DateTime.Now;

        /// <summary>
        /// todo
        /// </summary>
        public int CommentsCount { get; set; } = 9;
    }
}
