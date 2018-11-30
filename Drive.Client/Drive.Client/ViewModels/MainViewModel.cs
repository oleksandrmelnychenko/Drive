using Drive.Client.Helpers;
using Drive.Client.Models.Arguments.Notifications;
using Drive.Client.Models.Identities.Device;
using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.Models.Notifications;
using Drive.Client.Services.DeviceUtil;
using Drive.Client.Services.Notifications;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.BottomTabViewModels;
using Drive.Client.ViewModels.BottomTabViewModels.Bookmark;
using Drive.Client.ViewModels.BottomTabViewModels.Home;
using Drive.Client.ViewModels.BottomTabViewModels.Search;
using Drive.Client.ViewModels.Popups;
using Microsoft.AppCenter.Analytics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Drive.Client.ViewModels {
    public sealed class MainViewModel : ContentPageBaseViewModel {

        private readonly IDeviceUtilService _deviceUtilService;
        private readonly INotificationService _notificationService;

        private CancellationTokenSource _registerClientDeviceInfoCancellationTokenSource = new CancellationTokenSource();

        UpdateAppVersionPopupViewModel _updateAppVersionPopupViewModel;
        public UpdateAppVersionPopupViewModel UpdateAppVersionPopupViewModel {
            get => _updateAppVersionPopupViewModel;
            private set {
                _updateAppVersionPopupViewModel?.Dispose();
                SetProperty(ref _updateAppVersionPopupViewModel, value);
            }
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public MainViewModel(
            IDeviceUtilService deviceUtilService,
            INotificationService notificationService) {

            _deviceUtilService = deviceUtilService;
            _notificationService = notificationService;

            BottomBarItems = new List<IBottomBarTab>() {
                DependencyLocator.Resolve<HomeViewModel>(),
                DependencyLocator.Resolve<SearchViewModel>(),
                DependencyLocator.Resolve<PostBuilderViewModel>(),
                DependencyLocator.Resolve<BookmarkViewModel>(),
                DependencyLocator.Resolve<ProfileViewModel>()};
            BottomBarItems.ForEach(bottomBarTab => bottomBarTab.InitializeAsync(this));

            if (string.IsNullOrEmpty(BaseSingleton<GlobalSetting>.Instance.MessagingDeviceToken)) {
                MessagingCenter.Subscribe<object>(this, "device_token", (sender) => {
                    MessagingCenter.Unsubscribe<object>(this, "device_token");

                    RegisterClientDeviceInfo();
                });
            }
            else {
                RegisterClientDeviceInfo();
            }

            SelectedBottomItemIndex = 1;

            UpdateAppVersionPopupViewModel = DependencyLocator.Resolve<UpdateAppVersionPopupViewModel>();
            UpdateAppVersionPopupViewModel.InitializeAsync(this);
        }

        public override void Dispose() {
            base.Dispose();

            BottomBarItems?.ForEach(bottomBarItem => bottomBarItem?.Dispose());
            UpdateAppVersionPopupViewModel?.Dispose();
            ResetCancellationTokenSource(ref _registerClientDeviceInfoCancellationTokenSource);
        }

        public override Task InitializeAsync(object navigationData) {
            UpdateAppVersionPopupViewModel.InitializeAsync(this);

            if (navigationData is BottomTabIndexArgs bottomTabIndexArgs) {
                try {
                    SelectedBottomItemIndex =
                        BottomBarItems.IndexOf(BottomBarItems?.FirstOrDefault(barItem => barItem.GetType().Equals(bottomTabIndexArgs.TargetTab)));
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERRROR:{ex.Message}");
                    Debugger.Break();
                }
            }

            BottomBarItems?.ForEach(bottomBarItem => bottomBarItem.InitializeAsync(navigationData));

            return base.InitializeAsync(navigationData);
        }

        protected override void OnSubscribeOnAppEvents() {
            base.OnSubscribeOnAppEvents();

            _notificationService.ReceivedResidentVehicleDetailInfo += OnNotificationServiceReceivedResidentVehicleDetailInfo;
            _notificationService.TryToResolveLastReceivedNotification();
        }

        protected override void OnUnsubscribeFromAppEvents() {
            base.OnUnsubscribeFromAppEvents();

            _notificationService.ReceivedResidentVehicleDetailInfo -= OnNotificationServiceReceivedResidentVehicleDetailInfo;
        }

        private async void RegisterClientDeviceInfo() {
            try {
                if (BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth) {
                    ResetCancellationTokenSource(ref _registerClientDeviceInfoCancellationTokenSource);
                    CancellationTokenSource cancellationTokenSource = _registerClientDeviceInfoCancellationTokenSource;

                    ClientHardware clientHardware = await _deviceUtilService.GetDeviceInfoAsync(cancellationTokenSource);
                    string jObject = JsonConvert.SerializeObject(clientHardware);
                    Analytics.TrackEvent(BaseSingleton<AzureMobileCenter>.Instance.RegisterClientDeviceInfoEventKey, new Dictionary<string, string> { { "messagingDeveceToken", clientHardware.MessagingDeviceToken } });

                    bool deviceRegistrationCompletion = await _deviceUtilService.RegisterClientDeviceInfoAsync(clientHardware, cancellationTokenSource);

                    if (!deviceRegistrationCompletion) {
                        UpdateAppVersionPopupViewModel.ShowPopupCommand.Execute(null);
                    }
                }
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERRROR:{ex.Message}");
                Debugger.Break();
            }
        }

        private void OnNotificationServiceReceivedResidentVehicleDetailInfo(object sender, ReceivedResidentVehicleDetailInfoArgs args) {
            try {
                IBottomBarTab bookmarkTab = BottomBarItems.FirstOrDefault(tab => tab is BookmarkViewModel);
                bookmarkTab.InitializeAsync(args);

                SelectedBottomItemIndex = BottomBarItems.IndexOf(bookmarkTab);
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERRROR:{ex.Message}");
                Debugger.Break();
            }
        }
    }
}
