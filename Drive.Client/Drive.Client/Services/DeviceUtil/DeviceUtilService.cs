using Drive.Client.Exceptions;
using Drive.Client.Helpers;
using Drive.Client.Models.Identities.Device;
using Drive.Client.Services.Identity;
using Drive.Client.Services.RequestProvider;
using Plugin.DeviceInfo;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Drive.Client.Services.DeviceUtil {
    public class DeviceUtilService : IDeviceUtilService {

        private readonly IRequestProvider _requestProvider;

        private readonly IIdentityService _identityService;

        public DeviceUtilService(IRequestProvider requestProvider, IIdentityService identityService) {
            _requestProvider = requestProvider;
            _identityService = identityService;
        }

        public async Task<bool> RegisterClientDeviceInfoAsync(ClientHardware clientHardware, CancellationTokenSource cancellationTokenSource) =>
            await Task.Run(async () => {
                bool responseCompletion = default(bool);

                string url = BaseSingleton<GlobalSetting>.Instance.RestEndpoints.ClientHardwareEndPoints.RegisterClientDevice;
                string accessToken = BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken;

                try {
                    responseCompletion = await _requestProvider.PostAsync<bool, ClientHardware>(url, clientHardware, accessToken);
                }
                catch (ServiceAuthenticationException ex) {
                    await _identityService.LogOutAsync();
                    throw ex;
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR: {ex.Message}");
                    responseCompletion = true;
                }

                return responseCompletion;
            }, cancellationTokenSource.Token);

        public Task<Location> GetDeviceLocationAsync(GeolocationAccuracy accuracy, TimeSpan timeout, CancellationTokenSource cancellationTokenSource) =>
            Task.Run(async () => {
                Location result = null;

                try {
                    GeolocationRequest request = new GeolocationRequest(accuracy);
                    request.Timeout = timeout;
                    result = await Geolocation.GetLocationAsync(request, cancellationTokenSource.Token);
                } catch (FeatureNotSupportedException fnsEx) {
                    // TODO: Handle not supported on device exception
                    Debug.WriteLine($"ERROR: {fnsEx.Message}");
                    Debugger.Break();
                } catch (PermissionException pEx) {
                    // TODO: Handle permission exception
                    Debug.WriteLine($"ERROR: {pEx.Message}");
                    Debugger.Break();
                } catch (Exception ex) {
                    // TODO: Unable to get location
                    Debug.WriteLine($"ERROR: {ex.Message}");
                    Debugger.Break();
                }

                return result;
            }, cancellationTokenSource.Token);

        public Task<ClientHardware> GetDeviceInfoAsync(CancellationTokenSource cancellationTokenSource) =>
            Task.Run(async () => {
                ClientHardware clientHardware = new ClientHardware() {
                    DeviceId = CrossDeviceInfo.Current.Id,
                    Model = CrossDeviceInfo.Current.Model,
                    Manufacturer = CrossDeviceInfo.Current.Manufacturer,
                    DeviceName = CrossDeviceInfo.Current.DeviceName,
                    Version = CrossDeviceInfo.Current.Version,
                    AppVersion = CrossDeviceInfo.Current.AppVersion,
                    AppBuild = CrossDeviceInfo.Current.AppBuild,
                    Platform = CrossDeviceInfo.Current.Platform,
                    Idiom = CrossDeviceInfo.Current.Idiom,
                    IsDevice = CrossDeviceInfo.Current.IsDevice,
                    VersionNumber = CrossDeviceInfo.Current.Version,
                    MessagingDeviceToken = BaseSingleton<GlobalSetting>.Instance.MessagingDeviceToken
                };

                Location location = null;

                try {
                    if (Device.RuntimePlatform == Device.iOS) {
                        location = await GetDeviceLocationAsync(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(9), cancellationTokenSource);
                    } else {
                        var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                        if (status != PermissionStatus.Granted) {
                            if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location)) {
                                Debugger.Break();
                            }

                            var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                            if (results.ContainsKey(Permission.Location))
                                status = results[Permission.Location];
                        }

                        if (status == PermissionStatus.Granted) {
                            try {
                                //todo
                                location = await GetDeviceLocationAsync(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(9), cancellationTokenSource);
                            } catch (Exception ex) {
                                Debug.WriteLine($"ERROR:{ex.Message}");
                                Debugger.Break();
                            }

                        } else if (status != PermissionStatus.Unknown) {

                        }
                    }
                } catch (Exception ex) {
                    Debug.WriteLine($"ERROR:{ex.Message}");
                    Debugger.Break();
                }

                if (location != null) {
                    clientHardware.Latitude = location.Latitude;
                    clientHardware.Longitude = location.Longitude;
                    clientHardware.TimestampUtc = location.TimestampUtc.UtcDateTime.ToString();
                } else {
                    clientHardware.TimestampUtc = DateTime.UtcNow.ToString();
                }

                return clientHardware;
            }, cancellationTokenSource.Token);
    }
}
