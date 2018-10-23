using Drive.Client.Helpers.AppEvents.Events.Args;
using System;

namespace Drive.Client.Helpers.AppEvents.Events {
    public class LanguageEvents {
        public event EventHandler<LanguageChangedArgs> LanguageChanged = delegate { };

        public void OnLanguageChanged(string languageId) => LanguageChanged(this, new LanguageChangedArgs { LanguageId = languageId });
    }
}
