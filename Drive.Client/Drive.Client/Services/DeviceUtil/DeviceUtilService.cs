using Drive.Client.Helpers;
using Drive.Client.Models.Identities.Device;
using Drive.Client.Services.RequestProvider;
using Plugin.DeviceInfo;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Drive.Client.Services.DeviceUtil {
    public class DeviceUtilService : IDeviceUtilService {

        private readonly IRequestProvider _requestProvider;

        public DeviceUtilService(IRequestProvider requestProvider) {
            _requestProvider = requestProvider;
        }

        public Task<bool> RegisterClientDeviceInfoAsync(ClientHardware clientHardware, CancellationTokenSource cancellationTokenSource) =>
            Task.Run(async () => {
                bool responseCompletion = default(bool);

                try {
                    responseCompletion = await _requestProvider.PostAsync<bool, ClientHardware>(BaseSingleton<GlobalSetting>.Instance.RestEndpoints.ClientHardwareEndPoints.RegisterClientDevice, clientHardware);
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR: {ex.Message}");
                    Debugger.Break();
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
                }
                catch (FeatureNotSupportedException fnsEx) {
                    // TODO: Handle not supported on device exception
                    Debug.WriteLine($"ERROR: {fnsEx.Message}");
                    Debugger.Break();
                }
                catch (PermissionException pEx) {
                    // TODO: Handle permission exception
                    Debug.WriteLine($"ERROR: {pEx.Message}");
                    Debugger.Break();
                }
                catch (Exception ex) {
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
                    VersionNumber = CrossDeviceInfo.Current.Version
                };

                Location location;

                if (Device.RuntimePlatform == Device.Android) {
                    location = null;
                } else {
                    location = await GetDeviceLocationAsync(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(9), cancellationTokenSource);
                }

                if (location != null) {
                    clientHardware.Latitude = location.Latitude;
                    clientHardware.Longitude = location.Longitude;
                    clientHardware.TimestampUtc = location.TimestampUtc.UtcDateTime.ToString();
                }
                else {
                    clientHardware.TimestampUtc = DateTime.UtcNow.ToString();
                }

                return clientHardware;
            }, cancellationTokenSource.Token);
    }
}
