using System;

namespace Drive.Client.Helpers.AppEvents.Events.Args {
    public class LanguageChangedArgs : EventArgs {
        public string Language { get; set; }
    }
}
