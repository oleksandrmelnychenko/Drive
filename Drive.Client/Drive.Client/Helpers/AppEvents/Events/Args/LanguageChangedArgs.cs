using Drive.Client.Models.Identities.AppInterface.Language;
using System;

namespace Drive.Client.Helpers.AppEvents.Events.Args {
    public class LanguageChangedArgs : EventArgs {
        public AppLanguage AppLanguage { get; set; }
    }
}
