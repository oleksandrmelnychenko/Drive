using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Views.Popups;
using System;

namespace Drive.Client.ViewModels.Popups {
    public class PolandRequestInfoPopupViewModel : RequestInfoPopupBaseViewModel {

        private PolandVehicleDetail _lastIncomingVechile;

        public PolandRequestInfoPopupViewModel() {
            MainTitle = COMMON_REQUEST_INFO_MAIN_TITLE;
        }

        public override Type RelativeViewType => typeof(RequestInfoPopupView);

        protected async override void OnIsPopupVisible() {
            base.OnIsPopupVisible();

            if (!IsPopupVisible) {

                if (_lastIncomingVechile != null) {
                    await NavigationService.NavigateToAsync<PolandDriveAutoDetailsViewModel>(_lastIncomingVechile);
                }
                else {
                    await NavigationService.NavigateToAsync<MainViewModel>();
                }

                _lastIncomingVechile = null;
            }
        }

        protected override void OnShowPopupCommand(object param) {
            base.OnShowPopupCommand(param);

            if (param is PolandVehicleDetail polandVehicleDetail) {
                _lastIncomingVechile = polandVehicleDetail;
                PlainOutputText = VEHICLE_FOUND_OUTPUT;
            }
            else {
                _lastIncomingVechile = null;
                PlainOutputText = NO_RESULTS_OUTPUT;
            }
        }
    }
}
