using Drive.Client.Extensions;
using Drive.Client.Factories.Vehicle;
using Drive.Client.Helpers;
using Drive.Client.Helpers.Localize;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.Models.Arguments.Notifications;
using Drive.Client.Models.DataItems.Vehicle;
using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Models.EntityModels.Vehicle;
using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.Resources.Resx;
using Drive.Client.Services.Vehicle;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.IdentityAccounting.Registration;
using Drive.Client.Views.BottomTabViews.Bookmark;
using Microsoft.AppCenter.Crashes;
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

        private CancellationTokenSource _getRequestAsyncCancellationTokenSource = new CancellationTokenSource();

        private readonly IVehicleFactory _vehicleFactory;

        private readonly IVehicleService _vehicleService;

        private ReceivedResidentVehicleDetailInfoArgs _lastNotificationRequest;

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
                        if (polandRequestDataItem.PolandVehicleRequest.IsParsed) {
                            OnPolandRequestDataItem(polandRequestDataItem);
                        }
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
                GetRequests();
            }
            else if (navigationData is ReceivedResidentVehicleDetailInfoArgs vehicleDetailInfoArgs) {
                _lastNotificationRequest = vehicleDetailInfoArgs;
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
            ResetCancellationTokenSource(ref _getRequestAsyncCancellationTokenSource);
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

        private async void GetRequests() {
            try {
                if (BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth) {

                    UserRequests = (await GetRequestAsync()).ToObservableCollection();

                    if (_lastNotificationRequest != null) {
                        long govRequestId = 0;

                        if (long.TryParse(_lastNotificationRequest.RecidentVehicleNotification.Data, out govRequestId)) {

                            ResidentRequestDataItem requestDataItem = UserRequests?.OfType<ResidentRequestDataItem>().FirstOrDefault<ResidentRequestDataItem>(residentRequestItem => residentRequestItem.ResidentRequest.GovRequestId == govRequestId);

                            if (requestDataItem != null) {
                                GetVehicles(requestDataItem);
                            }
                        }

                        _lastNotificationRequest = null;
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
            GetRequests();
        }

        private Task<List<BaseRequestDataItem>> GetRequestAsync() =>
            Task<List<BaseRequestDataItem>>.Run(async () => {
                ResetCancellationTokenSource(ref _getRequestAsyncCancellationTokenSource);
                CancellationTokenSource cancellationTokenSource = _getRequestAsyncCancellationTokenSource;

                List<BaseRequestDataItem> createdItems = null;

                try {
                    List<ResidentRequest> userRequests = await _vehicleService.GetUserVehicleDetailRequestsAsync(cancellationTokenSource.Token);

                    if (userRequests != null) {
                        createdItems = _vehicleFactory.BuildResidentRequestItems(userRequests);

                        List<PolandVehicleRequest> polandVehicleRequests = await _vehicleService.GetPolandVehicleRequestsAsync(cancellationTokenSource.Token);
                        if (polandVehicleRequests != null) {
                            createdItems.AddRange(_vehicleFactory.BuildPolandRequestItems(polandVehicleRequests));
                        }
                        createdItems = createdItems.OrderByDescending(x => x.Created).ToList();

                        cancellationTokenSource.Token.ThrowIfCancellationRequested();
                        //Device.BeginInvokeOnMainThread(() => { UserRequests = createdItems.ToObservableCollection(); });
                    }
                }
                catch (OperationCanceledException) { }
                catch (ObjectDisposedException) { }
                catch (Exception exc) {
                    Debug.WriteLine($"ERROR: {exc.Message}");

                    createdItems = new List<BaseRequestDataItem>();
                }

                return createdItems;
            });

        //private async void OnInitializeVehicleDetailInfoArgs(ReceivedResidentVehicleDetailInfoArgs vehicleDetailInfoArgs) {
        //    try {
        //        if (BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth) {

        //            if (UserRequests == null || !UserRequests.Any()) {
        //                UserRequests = (await GetRequestAsync()).ToObservableCollection();
        //            }

        //            long govRequestId = 0;

        //            if (long.TryParse(vehicleDetailInfoArgs.RecidentVehicleNotification.Data, out govRequestId)) {

        //                ResidentRequestDataItem requestDataItem = UserRequests?.OfType<ResidentRequestDataItem>().FirstOrDefault<ResidentRequestDataItem>(residentRequestItem => residentRequestItem.ResidentRequest.GovRequestId == govRequestId);

        //                if (requestDataItem != null) {
        //                    GetVehicles(requestDataItem);
        //                }
        //            }
        //        }
        //    }
        //    catch (OperationCanceledException) { }
        //    catch (ObjectDisposedException) { }
        //    catch (Exception exc) {
        //        Crashes.TrackError(exc);
        //    }
        //}
    }
}
