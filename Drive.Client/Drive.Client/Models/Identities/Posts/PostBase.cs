using Drive.Client.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.Models.Identities.Posts {
    public abstract class PostBase : ExtendedBindableObject {
        /// <summary>
        /// Type of current post (text, media, link)
        /// </summary>
        public PostType PostType { get; protected set; }

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
        /// Comments count.
        /// </summary>
        int _commentsCount;
        public int CommentsCount {
            get { return _commentsCount; }
            set { SetProperty(ref _commentsCount, value); }
        }
    }
}
