using Drive.Client.Models.DataItems.ProfileSettings;
using System.Collections.Generic;

namespace Drive.Client.DataItems.ProfileSettings {
    public interface IProfileSettingsDataItems {

        List<LanguageDataItem> BuildLanguageDataItems();
    }
}
