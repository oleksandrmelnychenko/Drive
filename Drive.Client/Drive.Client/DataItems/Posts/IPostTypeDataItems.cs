using Drive.Client.Models.DataItems.SelectPostTypes;
using System.Collections.Generic;

namespace Drive.Client.DataItems.Posts {
    public interface IPostTypeDataItems {
        List<PostTypeDataItem> BuildLanguageDataItems();
    }
}
