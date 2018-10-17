using Drive.Client.Helpers.AppEvents.Events;
using Drive.Client.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.Helpers.AppEvents {
    public class AppMessagingEvents {

        public LanguageEvents LanguageEvents => DependencyLocator.Resolve<LanguageEvents>();

        public AppMessagingEvents() {

        }

        ~AppMessagingEvents() {

        }
    }
}
