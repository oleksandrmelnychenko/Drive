using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.Models.Identities.Posts {
    public abstract class PostBase {
        /// <summary>
        /// Post date created.
        /// </summary>
        public DateTime PostCreated { get; set; }

        /// <summary>
        /// Type of current post (text, media, link)
        /// </summary>
        public PostTypes PostType { get; protected set; }
    }
}
