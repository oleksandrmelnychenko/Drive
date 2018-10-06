using Drive.Client.Exceptions;
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

                string url = string.Format(BaseSingleton<GlobalSetting>.Instance.RestEndpoints.IdentityEndpoints.CheckPhoneNumberEndPoint, phoneNumber);

                try {
                    phoneNumberAvailabilty = await _requestProvider.GetAsync<PhoneNumberAvailabilty>(url);
                }
                catch (ConnectivityException ex) {
                    throw ex;
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
                catch (ConnectivityException ex) {
                    throw ex;
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR:{ex.Message}");
                    Debugger.Break();
                }

                return userNameAvailability;
            }, cancellationToken);

        public async Task<AuthenticationResult> SignUpAsync(RegistrationCollectedInputsArgs collectedInputsArgs, CancellationToken cancellationToken = default(CancellationToken)) =>
            await Task.Run(async () => {
                AuthenticationResult authenticationResult = null;

                string url = BaseSingleton<GlobalSetting>.Instance.RestEndpoints.IdentityEndpoints.SignUpEndPoint;

                try {
                    authenticationResult = await _requestProvider.PostAsync<AuthenticationResult, RegistrationCollectedInputsArgs>(url, collectedInputsArgs);

                    if (authenticationResult != null && authenticationResult.IsSucceed) {
                        SetupProfile(authenticationResult);
                    }
                }
                catch (ConnectivityException ex) {
                    throw ex;
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR:{ex.Message}");
                    Debugger.Break();
                }

                return authenticationResult;
            }, cancellationToken);



        public async Task<AuthenticationResult> SignInAsync(SignInArgs signInArgsArgs, CancellationToken cancellationToken = default(CancellationToken)) =>
            await Task.Run(async () => {
                AuthenticationResult authenticationResult = null;

                string url = BaseSingleton<GlobalSetting>.Instance.RestEndpoints.IdentityEndpoints.SignInEndPoint;

                try {
                    authenticationResult = await _requestProvider.PostAsync<AuthenticationResult, SignInArgs>(url, signInArgsArgs);

                    if (authenticationResult != null && authenticationResult.IsSucceed) {
                        SetupProfile(authenticationResult);
                    }
                }
                catch (ConnectivityException ex) {
                    throw ex;
                }catch(HttpRequestExceptionEx ex) {
                    throw ex;
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR:{ex.Message}");
                    Debugger.Break();
                }

                return authenticationResult;
            }, cancellationToken);

        private static void SetupProfile(AuthenticationResult authenticationResult) {
            try {
                BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken = authenticationResult.AccessToken;
                BaseSingleton<GlobalSetting>.Instance.UserProfile.RefreshToken = authenticationResult.RefreshToken;
                BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth = true;
                BaseSingleton<GlobalSetting>.Instance.UserProfile.NetId = authenticationResult.User.NetId;
                BaseSingleton<GlobalSetting>.Instance.UserProfile.PhoneNumber = authenticationResult.User.PhoneNumber;
                BaseSingleton<GlobalSetting>.Instance.UserProfile.Email = authenticationResult.User?.Email;
                BaseSingleton<GlobalSetting>.Instance.UserProfile.UserName = authenticationResult.User.UserName;
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR:{ex.Message}");
                Debugger.Break();
            }
        }
    }
}
