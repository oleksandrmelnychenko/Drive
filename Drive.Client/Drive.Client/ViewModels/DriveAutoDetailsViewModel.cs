using Drive.Client.Models.EntityModels.Search;
using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels {
    public sealed class DriveAutoDetailsViewModel : ContentPageBaseViewModel {

        bool _isBackButtonAvailable;
        public bool IsBackButtonAvailable {
            get { return _isBackButtonAvailable; }
            set { SetProperty(ref _isBackButtonAvailable, value); }
        }

        DriveAuto _driveAuto;
        public DriveAuto DriveAuto {
            get => _driveAuto;
            private set => SetProperty(ref _driveAuto, value);
        }

        /// <summary>
        ///     ctor();
        /// </summary>
        public DriveAutoDetailsViewModel() {
            ActionBarViewModel = DependencyLocator.Resolve<CommonActionBarViewModel>();

            IsBackButtonAvailable = NavigationService.IsBackButtonAvailable;
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is DriveAuto driveAuto) {
                DriveAuto = driveAuto;
            }

            return base.InitializeAsync(navigationData);
        }
    }
}
