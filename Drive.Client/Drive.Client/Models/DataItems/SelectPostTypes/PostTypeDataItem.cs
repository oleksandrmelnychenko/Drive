using Drive.Client.Models.Identities.Posts;

namespace Drive.Client.Models.DataItems.SelectPostTypes {
    public class PostTypeDataItem {
        public string Title { get; set; }

        public string Icon { get; set; }

        public PostType PostType { get; set; }
    }
}
