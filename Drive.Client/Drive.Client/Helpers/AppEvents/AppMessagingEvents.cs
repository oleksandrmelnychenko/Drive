using Drive.Client.Helpers.AppEvents.Events;
using Drive.Client.ViewModels.Base;

namespace Drive.Client.Helpers.AppEvents {
    public class AppMessagingEvents {

        public LanguageEvents LanguageEvents => DependencyLocator.Resolve<LanguageEvents>();

        public VehicleEvents VehicleEvents => DependencyLocator.Resolve<VehicleEvents>();
    }
}
