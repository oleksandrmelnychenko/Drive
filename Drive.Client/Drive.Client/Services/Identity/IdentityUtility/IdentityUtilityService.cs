﻿using Drive.Client.Exceptions;
using Drive.Client.Helpers;
using Drive.Client.Models.EntityModels.Identity;
using Drive.Client.Services.Dialog;
using Drive.Client.Services.Navigation;
using Drive.Client.Services.RequestProvider;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Drive.Client.Services.Identity.IdentityUtility {
    public class IdentityUtilityService : IIdentityUtilityService {

        private readonly IDialogService _dialogService;

        private readonly IRequestProvider _requestProvider;

        private readonly INavigationService _navigationService;

        /// <summary>
        ///     ctor().
        /// </summary>
        public IdentityUtilityService(INavigationService navigationService, IRequestProvider requestProvider, IDialogService dialogService) {
            _dialogService = dialogService;
            _requestProvider = requestProvider;
            _navigationService = navigationService;
        }

        public async Task LogOutAsync() {
            await Task.Run(async () => {
                try {
                    string url = BaseSingleton<GlobalSetting>.Instance.RestEndpoints.IdentityEndpoints.LogOutEndPoint;
                    string accessToken = BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken;

                    LogOutResult logOutResult = await _requestProvider.PostAsync<LogOutResult, object>(url, null, accessToken);

                    if (logOutResult != null) { }
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR LogOutAsync():{ex.Message}");
                }
            });

            BaseSingleton<GlobalSetting>.Instance.UserProfile.ClearUserProfile();
            BaseSingleton<GlobalSetting>.Instance.UserProfile.SaveChanges();
            await _navigationService.InitializeAsync();
        }
    }
}
