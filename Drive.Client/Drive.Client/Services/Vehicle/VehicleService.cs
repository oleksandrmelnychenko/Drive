using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Drive.Client.Exceptions;
using Drive.Client.Helpers;
using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Models.EntityModels.Vehicle;
using Drive.Client.Models.Identities.NavigationArgs;
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

        public Task<List<ResidentRequest>> GetUserVehicleDetailRequestsAsync(CancellationToken cancellationToken = default(CancellationToken)) =>
            Task.Run(async () => {
                List<ResidentRequest> residentRequests = null;

                string url = BaseSingleton<GlobalSetting>.Instance.RestEndpoints.VehicleEndpoints.UserVehicleDetailRequestsEndpoint;
                string accessToken = BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken;

                try {
                    residentRequests = await _requestProvider.GetAsync<List<ResidentRequest>>(url, accessToken, cancellationToken);
                }
                catch (ConnectivityException ex) {
                    throw ex;
                }
                catch (HttpRequestExceptionEx ex) {
                    throw ex;
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR:{ex.Message}");
                    throw ex;
                }

                return residentRequests;
            }, cancellationToken);

        public async Task<List<VehicleDetail>> GetVehiclesByRequestIdAsync(long govRequestId, CancellationToken cancellationToken = default(CancellationToken)) =>
            await Task.Run(async () => {
                List<VehicleDetail> vehicleDetails = null;

                string url = string.Format(BaseSingleton<GlobalSetting>.Instance.RestEndpoints.VehicleEndpoints.VehicleDetailsEndpoint, govRequestId);
                string accessToken = BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken;

                try {
                    vehicleDetails = await _requestProvider.GetAsync<List<VehicleDetail>>(url, accessToken);
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
                return vehicleDetails;
            }, cancellationToken);

        public async Task<VehicleDetailsByResidentFullName> GetVehicleDetailsByResidentFullNameAsync(SearchByPersonArgs searchByPersonArgs, CancellationToken cancellationToken = default(CancellationToken)) =>
            await Task.Run(async () => {
                VehicleDetailsByResidentFullName vehicleDetailsByResidentFullName = null;

                string url = string.Format(BaseSingleton<GlobalSetting>.Instance.RestEndpoints.VehicleEndpoints.VehicleDetailsByResidentFullNameEndpoint,
                    searchByPersonArgs.FirstName, searchByPersonArgs.LastName, searchByPersonArgs.MiddleName, searchByPersonArgs.DateOfBirth);
                string accessToken = BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken;

                try {
                    vehicleDetailsByResidentFullName = await _requestProvider.GetAsync<VehicleDetailsByResidentFullName>(url, accessToken);
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
                return vehicleDetailsByResidentFullName;
            }, cancellationToken);
    }
}
