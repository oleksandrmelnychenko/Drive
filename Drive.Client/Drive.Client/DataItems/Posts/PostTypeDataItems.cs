using Drive.Client.Helpers.Localize;
using Drive.Client.Models.DataItems.SelectPostTypes;
using Drive.Client.Models.Identities.Posts;
using Drive.Client.Resources.Resx;
using System.Collections.Generic;

namespace Drive.Client.DataItems.Posts {
    internal class PostTypeDataItems : IPostTypeDataItems {

        public List<PostTypeDataItem> BuildLanguageDataItems(ResourceLoader resourceLoader) =>
            new List<PostTypeDataItem> {
                new PostTypeDataItem {
                    Icon = "resource://Drive.Client.Resources.Images.photos.svg",
                    Title = resourceLoader[nameof(AppStrings.PostWithPhoto)],
                    PostType = PostType.MediaPost
                },
                new PostTypeDataItem {
                    Icon = "resource://Drive.Client.Resources.Images.text.svg",
                    Title = resourceLoader[nameof(AppStrings.TextPost)],
                    PostType = PostType.TextPost
                }
            };
    }
}
