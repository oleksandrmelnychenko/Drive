using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.Services.DeviceUtil;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.BottomTabViewModels;
using Drive.Client.ViewModels.BottomTabViewModels.Bookmark;
using Drive.Client.ViewModels.BottomTabViewModels.Search;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels {
    public sealed class MainViewModel : ContentPageBaseViewModel {

        private readonly IDeviceUtilService _deviceUtilService;

        private CancellationTokenSource _registerClientDeviceInfoCancellationTokenSource = new CancellationTokenSource();

        /// <summary>
        ///     ctor().
        /// </summary>
        public MainViewModel(IDeviceUtilService deviceUtilService) {
            _deviceUtilService = deviceUtilService;

            BottomBarItems = new List<IBottomBarTab>() {
                DependencyLocator.Resolve<HomeViewModel>(),
                DependencyLocator.Resolve<SearchViewModel>(),
                DependencyLocator.Resolve<PostViewModel>(),
                DependencyLocator.Resolve<BookmarkViewModel>(),
                DependencyLocator.Resolve<ProfileViewModel>()};
            BottomBarItems.ForEach(bottomBarTab => bottomBarTab.InitializeAsync(this));

            RegisterClientDeviceInfo();

            SelectedBottomItemIndex = 1;
        }

        public override void Dispose() {
            base.Dispose();

            BottomBarItems?.ForEach(bottomBarItem => bottomBarItem?.Dispose());
            ResetCancellationTokenSource(ref _registerClientDeviceInfoCancellationTokenSource);
        }

        public override Task InitializeAsync(object navigationData) {

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

        private async void RegisterClientDeviceInfo() {
            ResetCancellationTokenSource(ref _registerClientDeviceInfoCancellationTokenSource);
            CancellationTokenSource cancellationTokenSource = _registerClientDeviceInfoCancellationTokenSource;

            try {
                string deviceRegistrationCompletion = await _deviceUtilService.RegisterClientDeviceInfoAsync(await _deviceUtilService.GetDeviceInfoAsync(cancellationTokenSource), cancellationTokenSource);
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERRROR:{ex.Message}");
                Debugger.Break();
            }
        }
    }
}
