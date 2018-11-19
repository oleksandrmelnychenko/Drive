using Drive.Client.Models.DataItems.SelectPostTypes;
using Drive.Client.Models.Identities.Posts;
using System.Collections.Generic;

namespace Drive.Client.DataItems.Posts {
    internal class PostTypeDataItems : IPostTypeDataItems {

        public List<PostTypeDataItem> BuildLanguageDataItems() =>
            new List<PostTypeDataItem> {
                new PostTypeDataItem {
                    Icon = "resource://Drive.Client.Resources.Images.photos.svg",
                    Title ="Пост з фото",
                    PostType = PostType.MediaPost
                },
                new PostTypeDataItem {
                    Icon = "resource://Drive.Client.Resources.Images.text.svg",
                    Title ="Пост текст",
                    PostType = PostType.TextPost
                }
            };
    }
}
