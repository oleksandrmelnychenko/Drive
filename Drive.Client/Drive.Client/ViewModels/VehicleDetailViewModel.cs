using Drive.Client.Extensions;
using Drive.Client.Models.DataItems.Vehicle;
using Drive.Client.Models.EntityModels.Vehicle;
using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels {
    public sealed class VehicleDetailViewModel : ContentPageBaseViewModel {

        bool _isBackButtonAvailable;
        public bool IsBackButtonAvailable {
            get { return _isBackButtonAvailable; }
            set { SetProperty(ref _isBackButtonAvailable, value); }
        }

        ResidentRequestDataItem _residentRequestDataItem;
        public ResidentRequestDataItem ResidentRequestDataItem {
            get { return _residentRequestDataItem; }
            set { SetProperty(ref _residentRequestDataItem, value); }
        }

        ObservableCollection<VehicleDetail> _vehicleDetails;
        public ObservableCollection<VehicleDetail> VehicleDetails {
            get { return _vehicleDetails; }
            set { SetProperty(ref _vehicleDetails, value); }
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public VehicleDetailViewModel() {
            ActionBarViewModel = DependencyLocator.Resolve<CommonActionBarViewModel>();

            IsBackButtonAvailable = NavigationService.IsBackButtonAvailable;
        }

        public override void Dispose() {
            base.Dispose();

            VehicleDetails?.Clear();
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is VehicleArgs vehicleArgs) {
                ResidentRequestDataItem = vehicleArgs.ResidentRequestDataItem;
                VehicleDetails = vehicleArgs?.VehicleDetails.ToObservableCollection();
            }

            return base.InitializeAsync(navigationData);
        }
    }
}
