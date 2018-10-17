using Drive.Client.Helpers.AppEvents.Events.Args;
using System;

namespace Drive.Client.Helpers.AppEvents.Events {
    public class LanguageEvents {

        public event EventHandler<LanguageChangedArgs> LanguageChanged = delegate { };

        public LanguageEvents() {

        }

        ~LanguageEvents() {

        }

        public void OnLanguageChanged(string language) => LanguageChanged(this, new LanguageChangedArgs { Language = language });

    }
}
