using System;

namespace Drive.Client.Helpers.AppEvents.Events.Args {
    public class LanguageChangedArgs : EventArgs {
        public string LanguageId { get; set; }
    }
}
