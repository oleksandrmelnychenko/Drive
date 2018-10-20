using Drive.Client.Extensions;
using Drive.Client.Factories.Vehicle;
using Drive.Client.Helpers;
using Drive.Client.Helpers.Localize;
using Drive.Client.Models.DataItems.Vehicle;
using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Models.EntityModels.Vehicle;
using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.Resources.Resx;
using Drive.Client.Services.Vehicle;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.IdentityAccounting.Registration;
using Drive.Client.Views.BottomTabViews.Bookmark;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Drive.Client.ViewModels.BottomTabViewModels.Bookmark {
    public sealed class UserVehiclesViewModel : NestedViewModel, IVisualFiguring {

        private CancellationTokenSource _getUserVehicleDetailRequestsCancellationTokenSource = new CancellationTokenSource();

        private CancellationTokenSource _getVehiclesCancellationTokenSource = new CancellationTokenSource();

        private readonly IVehicleFactory _vehicleFactory;

        private readonly IVehicleService _vehicleService;

        public Type RelativeViewType => typeof(UserVehiclesView);

        StringResource _tabHeader = Helpers.Localize.ResourceLoader.Instance.GetString(nameof(AppStrings.HistoryRequestsUpperCase));
        public StringResource TabHeader {
            get => _tabHeader;
            private set => SetProperty(ref _tabHeader, value);
        }

        bool _visibilityClosedView;
        public bool VisibilityClosedView {
            get { return _visibilityClosedView; }
            set { SetProperty(ref _visibilityClosedView, value); }
        }

        ObservableCollection<ResidentRequestDataItem> _userRequests;
        public ObservableCollection<ResidentRequestDataItem> UserRequests {
            get { return _userRequests; }
            set {
                _userRequests?.ForEach(r => r.Dispose());
                SetProperty(ref _userRequests, value);
            }
        }

        ResidentRequestDataItem _selectedRequest;
        public ResidentRequestDataItem SelectedRequest {
            get { return _selectedRequest; }
            set {
                if (SetProperty(ref _selectedRequest, value) && value != null) {
                    if (value.ResidentRequest.VehicleCount > 0) {
                        GetVehicles(value);
                    }
                }
            }
        }

        public ICommand SignInCommand => new Command(async () => await NavigationService.NavigateToAsync<SignInPhoneNumberStepViewModel>());

        public ICommand SignUpCommand => new Command(async () => await NavigationService.NavigateToAsync<PhoneNumberRegisterStepViewModel>());

        /// <summary>
        ///     ctor().
        /// </summary>
        public UserVehiclesViewModel(IVehicleService vehicleService, IVehicleFactory vehicleFactory) {
            _vehicleFactory = vehicleFactory;
            _vehicleService = vehicleService;

            //GetRequestsAsync();
        }

        public override Task InitializeAsync(object navigationData) {

            GetRequestsAsync();

            UpdateView();

            return base.InitializeAsync(navigationData);
        }

        public override void Dispose() {
            base.Dispose();

            UserRequests?.ForEach(r => r.Dispose());
            UserRequests?.Clear();

            ResetCancellationTokenSource(ref _getUserVehicleDetailRequestsCancellationTokenSource);
            ResetCancellationTokenSource(ref _getVehiclesCancellationTokenSource);
        }

        private async void GetVehicles(ResidentRequestDataItem residentRequestDataItem) {
            IEnumerable<VehicleDetail> foundVehicles = await GetVehiclesByRequestIdAsync(residentRequestDataItem.ResidentRequest.GovRequestId);

            if (foundVehicles != null && foundVehicles.Any()) {
                VehicleArgs vehicleArgs = new VehicleArgs {
                    ResidentRequestDataItem = residentRequestDataItem,
                    VehicleDetails = foundVehicles
                };

                await NavigationService.NavigateToAsync<VehicleDetailViewModel>(vehicleArgs);
            }
        }

        private async Task<IEnumerable<VehicleDetail>> GetVehiclesByRequestIdAsync(long govRequestId) {
            ResetCancellationTokenSource(ref _getVehiclesCancellationTokenSource);
            CancellationTokenSource cancellationTokenSource = _getVehiclesCancellationTokenSource;

            IEnumerable<VehicleDetail> result = null;

            Guid busyKey = Guid.NewGuid();
            UpdateBusyVisualState(busyKey, true);

            try {
                result = await _vehicleService.GetVehiclesByRequestIdAsync(govRequestId, cancellationTokenSource.Token);
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR: {ex.Message}");
                Debugger.Break();
            }

            UpdateBusyVisualState(busyKey, false);

            return result;
        }

        private void UpdateView() {
            VisibilityClosedView = !BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth;
        }

        private async void GetRequestsAsync() {
            ResetCancellationTokenSource(ref _getUserVehicleDetailRequestsCancellationTokenSource);
            CancellationTokenSource cancellationTokenSource = _getUserVehicleDetailRequestsCancellationTokenSource;

            try {
                if (BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth) {
                    List<ResidentRequest> userRequests =
                        await _vehicleService.GetUserVehicleDetailRequestsAsync(cancellationTokenSource.Token);

                    cancellationTokenSource.Token.ThrowIfCancellationRequested();
                    if (userRequests != null) {
                        var createdItems = _vehicleFactory.BuildItems(userRequests);

                        UserRequests = createdItems.ToObservableCollection();
                    }
                }
            }
            catch (OperationCanceledException) { }
            catch (ObjectDisposedException) { }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR:{ex.Message}");
                Debugger.Break();
            }
        }
    }
}
