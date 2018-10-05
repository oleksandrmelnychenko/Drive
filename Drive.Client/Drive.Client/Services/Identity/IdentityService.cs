using Drive.Client.Extensions;
using Drive.Client.Helpers;
using Drive.Client.Models.Arguments.IdentityAccounting.Registration;
using Drive.Client.Models.EntityModels.Identity;
using Drive.Client.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.Identity {
    public class IdentityService : IIdentityService {

        private readonly IRequestProvider _requestProvider;

        /// <summary>
        ///     ctor().
        /// </summary>
        public IdentityService(IRequestProvider requestProvider) {
            _requestProvider = requestProvider;
        }

        public async Task<PhoneNumberAvailabilty> CheckPhoneNumberAvailabiltyAsync(string phoneNumber, CancellationToken cancellationToken) =>
            await Task.Run(async () => {
                PhoneNumberAvailabilty phoneNumberAvailabilty = null;

                string url = string.Format(BaseSingleton<GlobalSetting>.Instance.RestEndpoints.IdentityEndpoints.CheckPhoneNumberEndPoint, phoneNumber.EncodeQueryString());

                try {
                    phoneNumberAvailabilty = await _requestProvider.GetAsync<PhoneNumberAvailabilty>(url);
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR:{ex.Message}");
                    Debugger.Break();
                }

                return phoneNumberAvailabilty;
            }, cancellationToken);

        public async Task<UserNameAvailability> CheckUserNameAvailabiltyAsync(string userNmae, CancellationToken cancellationToken = default(CancellationToken)) =>
            await Task.Run(async () => {
                UserNameAvailability userNameAvailability = null;

                string url = string.Format(BaseSingleton<GlobalSetting>.Instance.RestEndpoints.IdentityEndpoints.CheckUserNameEndpoint, userNmae);

                try {
                    userNameAvailability = await _requestProvider.GetAsync<UserNameAvailability>(url);
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR:{ex.Message}");
                    Debugger.Break();
                }

                return userNameAvailability;
            }, cancellationToken);

        public async Task<SignUpResult> SignUpAsync(RegistrationCollectedInputsArgs collectedInputsArgs, CancellationToken cancellationToken = default(CancellationToken)) =>
            await Task.Run(async () => {
                SignUpResult signUpResult = null;

                string url = BaseSingleton<GlobalSetting>.Instance.RestEndpoints.IdentityEndpoints.SignUpEndPoint;

                try {
                    signUpResult = await _requestProvider.PostAsync<SignUpResult, RegistrationCollectedInputsArgs>(url, collectedInputsArgs);

                    if (signUpResult != null && signUpResult.IsSucceed) {
                        SetupProfile(signUpResult);
                    }
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR:{ex.Message}");
                    Debugger.Break();
                }

                return signUpResult;
            }, cancellationToken);

        private static void SetupProfile(SignUpResult signUpResult) {
            BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken = signUpResult.AccessToken;
            BaseSingleton<GlobalSetting>.Instance.UserProfile.RefreshToken = signUpResult.RefreshToken;
            BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth = true;
            BaseSingleton<GlobalSetting>.Instance.UserProfile.NetId = signUpResult.User.NetId;
            BaseSingleton<GlobalSetting>.Instance.UserProfile.PhoneNumber = signUpResult.User.PhoneNumber;
            BaseSingleton<GlobalSetting>.Instance.UserProfile.Email = signUpResult.User?.Email;
            BaseSingleton<GlobalSetting>.Instance.UserProfile.UserName = signUpResult.User.UserName;
        }
    }
}
