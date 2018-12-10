using Drive.Client.Helpers.Localize;
using Drive.Client.Models.EntityModels.Announcement;

namespace Drive.Client.Models.DataItems.SelectPostTypes {
    public class PostTypeDataItem {

        public StringResource Title { get; set; }

        public string Icon { get; set; }

        public AnnounceType PostType { get; set; }
    }
}
