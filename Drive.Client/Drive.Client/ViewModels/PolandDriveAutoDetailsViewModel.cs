using Drive.Client.Models.EntityModels.Search;
using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.Base;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels {
    public class PolandDriveAutoDetailsViewModel : ContentPageBaseViewModel {

        public PolandDriveAutoDetailsViewModel() {
            ActionBarViewModel = DependencyLocator.Resolve<CommonActionBarViewModel>();
            ActionBarViewModel.InitializeAsync(this);
        }

        PolandVehicleDetail _polandDriveAuto;
        public PolandVehicleDetail PolandDriveAuto {
            get => _polandDriveAuto;
            private set => SetProperty(ref _polandDriveAuto, value);
        }

        public override void Dispose() {
            base.Dispose();

            ActionBarViewModel?.Dispose();
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is PolandVehicleDetail polandVehicleDetail) {
                PolandDriveAuto = polandVehicleDetail;
            }

            ActionBarViewModel?.InitializeAsync(navigationData);

            return base.InitializeAsync(navigationData);
        }
    }
}
