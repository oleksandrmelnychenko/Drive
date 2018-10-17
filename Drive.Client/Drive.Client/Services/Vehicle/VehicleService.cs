using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Drive.Client.Exceptions;
using Drive.Client.Helpers;
using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Services.RequestProvider;

namespace Drive.Client.Services.Vehicle {
    public class VehicleService : IVehicleService {

        private readonly IRequestProvider _requestProvider;

        /// <summary>
        ///     ctor().
        /// </summary>
        public VehicleService(IRequestProvider requestProvider) {
            _requestProvider = requestProvider;
        }

        public async Task<List<ResidentRequest>> GetUserVehicleDetailRequestsAsync(CancellationToken cancellationToken = default(CancellationToken)) =>
            await Task.Run(async () => {
                List<ResidentRequest> residentRequests = null;

                string url = BaseSingleton<GlobalSetting>.Instance.RestEndpoints.VehicleEndpoints.UserVehicleDetailRequests;
                string accessToken = BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken;

                try {
                    residentRequests = await _requestProvider.GetAsync<List<ResidentRequest>>(url, accessToken);
                }
                catch (ConnectivityException ex) {
                    throw ex;
                }
                catch (HttpRequestExceptionEx ex) {
                    throw ex;
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR:{ex.Message}");
                    Debugger.Break();
                }
                return residentRequests;
            }, cancellationToken);
    }
}
