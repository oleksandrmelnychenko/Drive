﻿using Drive.Client.Services.DeviceUtil;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.BottomTabViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            RegisterClientDeviceInfo();

            SelectedBottomItemIndex = 1;
        }

        public override void Dispose() {
            base.Dispose();

            ResetCancellationTokenSource(ref _registerClientDeviceInfoCancellationTokenSource);
        }

        public override Task InitializeAsync(object navigationData) {

            BottomBarItems?.ForEach(bottomBarItem => bottomBarItem.InitializeAsync(navigationData));

            return base.InitializeAsync(navigationData);
        }

        private async void RegisterClientDeviceInfo() {
            ResetCancellationTokenSource(ref _registerClientDeviceInfoCancellationTokenSource);
            CancellationTokenSource cancellationTokenSource = _registerClientDeviceInfoCancellationTokenSource;

            try {
                string deviceRegistrationCompletion = await _deviceUtilService.RegisterClientDeviceInfoAsync(await _deviceUtilService.GetDeviceInfoAsync(cancellationTokenSource), cancellationTokenSource);
            }
            catch (Exception exc) {
                Debugger.Break();
            }
        }
    }
}
