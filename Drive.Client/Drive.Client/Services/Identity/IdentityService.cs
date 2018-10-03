using Drive.Client.Helpers;
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
        }
}
