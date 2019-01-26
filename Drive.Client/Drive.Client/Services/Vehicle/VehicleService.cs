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
using Drive.Client.Services.Identity;
using Drive.Client.Services.RequestProvider;

namespace Drive.Client.Services.Vehicle {
    public class VehicleService : IVehicleService {

        private readonly IRequestProvider _requestProvider;

        private readonly IIdentityService _identityService;

        /// <summary>
        ///     ctor().
        /// </summary>
        public VehicleService(IRequestProvider requestProvider, IIdentityService identityService) {
            _requestProvider = requestProvider;
            _identityService = identityService;
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
                catch (ServiceAuthenticationException ex) {
                    await _identityService.LogOutAsync();
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
                catch (ServiceAuthenticationException ex) {
                    await _identityService.LogOutAsync();
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
                catch (ServiceAuthenticationException ex) {
                    await _identityService.LogOutAsync();
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

        public async Task<PolandVehicleDetail> GetPolandVehicleDetails(SearchByPolandNumberArgs searchByPolandNumberArgs, CancellationToken cancellationToken = default(CancellationToken)) =>
             await Task.Run(async () => {
                 PolandVehicleDetail polandVehicleDetail = null;

                 string url = string.Format(BaseSingleton<GlobalSetting>.Instance.RestEndpoints.VehicleEndpoints.PolandVehicleDetailsEndpoint,
                     searchByPolandNumberArgs.Vin, searchByPolandNumberArgs.Date, searchByPolandNumberArgs.Number);
                 string accessToken = BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken;

                 try {
                     polandVehicleDetail = await _requestProvider.GetAsync<PolandVehicleDetail>(url, accessToken);
                 }
                 catch (ConnectivityException ex) {
                     throw ex;
                 }
                 catch (HttpRequestExceptionEx ex) {
                     polandVehicleDetail = null;
                 }
                 catch (ServiceAuthenticationException ex) {
                     await _identityService.LogOutAsync();
                     throw ex;
                 }
                 catch (Exception ex) {
                     Debug.WriteLine($"ERROR:{ex.Message}");
                     Debugger.Break();
                 }
                 return polandVehicleDetail;
             }, cancellationToken);

        public async Task<PolandVehicleDetail> GetPolandVehicleDetailsByRequestIdAsync(string requestId, CancellationToken cancellationToken = default(CancellationToken)) =>
            await Task.Run(async () => {
                PolandVehicleDetail polandVehicleDetail = null;

                string url =
                    string.Format(BaseSingleton<GlobalSetting>.Instance.RestEndpoints.VehicleEndpoints.PolandVehicleDetailsByRequestIdEndpoint, requestId);
                string accessToken = BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken;

                try {
                    polandVehicleDetail = await _requestProvider.GetAsync<PolandVehicleDetail>(url, accessToken);
                }
                catch (ConnectivityException ex) {
                    throw ex;
                }
                catch (ServiceAuthenticationException ex) {
                    await _identityService.LogOutAsync();
                    throw ex;
                }
                catch (HttpRequestExceptionEx ex) {
                    polandVehicleDetail = null;
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR:{ex.Message}");
                    Debugger.Break();
                }
                return polandVehicleDetail;
            }, cancellationToken);

        public async Task<List<PolandVehicleRequest>> GetPolandVehicleRequestsAsync(CancellationToken cancellationToken = default(CancellationToken)) =>
              await Task.Run(async () => {
                  List<PolandVehicleRequest> polandVehicleRequests = new List<PolandVehicleRequest>();

                  string url = BaseSingleton<GlobalSetting>.Instance.RestEndpoints.VehicleEndpoints.PolandVehicleRequestsEndpoint;
                  string accessToken = BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken;

                  try {
                      polandVehicleRequests = await _requestProvider.GetAsync<List<PolandVehicleRequest>>(url, accessToken);
                  }
                  catch (ConnectivityException ex) {
                      throw ex;
                  }
                  catch (ServiceAuthenticationException ex) {
                      await _identityService.LogOutAsync();
                      throw ex;
                  }
                  catch (HttpRequestExceptionEx ex) {
                      polandVehicleRequests = null;
                  }
                  catch (Exception ex) {
                      Debug.WriteLine($"ERROR:{ex.Message}");
                      Debugger.Break();
                  }
                  return polandVehicleRequests;
              }, cancellationToken);

        public async Task<List<CognitiveRequest>> GetCognitiveRequestsAsync(CancellationToken cancellationToken = default(CancellationToken)) =>
              await Task.Run(async () => {
                  List<CognitiveRequest> cognitiveRequests = new List<CognitiveRequest>();

                  string url = BaseSingleton<GlobalSetting>.Instance.RestEndpoints.VehicleEndpoints.CognitiveRequestsEndpoint;
                  string accessToken = BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken;

                  try {
                      cognitiveRequests = await _requestProvider.GetAsync<List<CognitiveRequest>>(url, accessToken);
                  }
                  catch (ConnectivityException ex) {
                      throw ex;
                  }
                  catch (ServiceAuthenticationException ex) {
                      await _identityService.LogOutAsync();
                      throw ex;
                  }
                  catch (HttpRequestExceptionEx ex) {
                      cognitiveRequests = null;
                  }
                  catch (Exception ex) {
                      Debug.WriteLine($"ERROR:{ex.Message}");
                      Debugger.Break();
                  }
                  return cognitiveRequests;
              }, cancellationToken);
    }
}
