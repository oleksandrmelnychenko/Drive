using Drive.Client.Helpers.AppEvents.Events.Args;
using Drive.Client.Models.Identities.AppInterface.Language;
using System;

namespace Drive.Client.Helpers.AppEvents.Events {
    public class LanguageEvents {
        public event EventHandler<LanguageChangedArgs> LanguageChanged = delegate { };

        public void OnLanguageChanged(AppLanguage appLanguage) => LanguageChanged(this, new LanguageChangedArgs { AppLanguage = appLanguage });
    }
}
