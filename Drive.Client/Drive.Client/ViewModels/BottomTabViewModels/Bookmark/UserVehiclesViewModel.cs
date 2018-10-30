using Drive.Client.Extensions;
using Drive.Client.Factories.Vehicle;
using Drive.Client.Helpers;
using Drive.Client.Helpers.Localize;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
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
    public sealed class UserVehiclesViewModel : NestedViewModel, IVisualFiguring, ISwitchTab {

        private CancellationTokenSource _getUserVehicleDetailRequestsCancellationTokenSource = new CancellationTokenSource();

        private CancellationTokenSource _getVehiclesCancellationTokenSource = new CancellationTokenSource();

        private CancellationTokenSource _getPolandVehicleInfoCancellationTokenSource = new CancellationTokenSource();

        private readonly IVehicleFactory _vehicleFactory;

        private readonly IVehicleService _vehicleService;

        private bool _canGet = true;

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

        ObservableCollection<BaseRequestDataItem> _userRequests;
        public ObservableCollection<BaseRequestDataItem> UserRequests {
            get { return _userRequests; }
            set {
                _userRequests?.ForEach(r => r.Dispose());
                SetProperty(ref _userRequests, value);
            }
        }

        BaseRequestDataItem _selectedRequest;
        public BaseRequestDataItem SelectedRequest {
            get { return _selectedRequest; }
            set {
                if (SetProperty(ref _selectedRequest, value) && value != null) {
                    if (value is ResidentRequestDataItem residentRequestDataItem) {
                        if (residentRequestDataItem.ResidentRequest.VehicleCount > 0) {
                            GetVehicles((ResidentRequestDataItem)value);
                        }
                    }
                    else if (value is PolandRequestDataItem polandRequestDataItem) {
                        OnPolandRequestDataItem(polandRequestDataItem);
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
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is SelectedBottomBarTabArgs) {
                GetRequestsAsync();
            }

            UpdateView();

            return base.InitializeAsync(navigationData);
        }

        public override void Dispose() {
            base.Dispose();

            UserRequests?.ForEach(r => r.Dispose());
            UserRequests?.Clear();

            ResetCancellationTokenSource(ref _getPolandVehicleInfoCancellationTokenSource);
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

        private async void OnPolandRequestDataItem(PolandRequestDataItem selectedPolandRequestDataItem) {
            Guid busyKey = Guid.NewGuid();
            UpdateBusyVisualState(busyKey, true);

            ResetCancellationTokenSource(ref _getPolandVehicleInfoCancellationTokenSource);
            CancellationTokenSource cancellationTokenSource = _getPolandVehicleInfoCancellationTokenSource;

            try {
                PolandVehicleDetail polandVehicleDetail = await _vehicleService.GetPolandVehicleDetailsByRequestIdAsync(selectedPolandRequestDataItem.PolandVehicleRequest.RequestId.ToString(), cancellationTokenSource.Token);

                if (polandVehicleDetail != null) {
                    UpdateBusyVisualState(busyKey, false);
                    await NavigationService.NavigateToAsync<PolandDriveAutoDetailsViewModel>(polandVehicleDetail);
                }
            }
            catch (OperationCanceledException) { }
            catch (ObjectDisposedException) { }
            catch (Exception exc) {
                UpdateBusyVisualState(busyKey, false);

                Debug.WriteLine($"ERROR: {exc.Message}");
                Debugger.Break();
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
            try {
                if (BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth) {

                    List<ResidentRequest> userRequests = await _vehicleService.GetUserVehicleDetailRequestsAsync();

                    if (userRequests != null) {
                        List<BaseRequestDataItem> createdItems = _vehicleFactory.BuildResidentRequestItems(userRequests);

                        List<PolandVehicleRequest> polandVehicleRequests = await _vehicleService.GetPolandVehicleRequestsAsync();
                        if (polandVehicleRequests != null) {
                            createdItems.AddRange(_vehicleFactory.BuildPolandRequestItems(polandVehicleRequests));
                        }
                        UserRequests = createdItems.OrderByDescending(x => x.Created).ToObservableCollection();
                    }
                }
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public void ClearAfterTabTap() {
        }

        public void TabClicked() {
            GetRequestsAsync();
        }
    }
}
