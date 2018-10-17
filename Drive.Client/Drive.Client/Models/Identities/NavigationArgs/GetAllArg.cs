using Drive.Client.Models.EntityModels.Search;
using System.Collections.ObjectModel;

namespace Drive.Client.Models.Identities.NavigationArgs {
    internal class GetAllArg {
        public string Value { get; set; }

        public ObservableCollection<DriveAuto> FoundCars { get; set; }
    }
}
