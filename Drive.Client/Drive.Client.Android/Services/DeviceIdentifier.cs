using Android.OS;
using Drive.Client.Droid.Services;
using Drive.Client.Models.Services.DeviceIdentifier;
using Drive.Client.Services.DeviceIdentifer;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceIdentifier))]
namespace Drive.Client.Droid.Services {
    internal class DeviceIdentifier : IDeviceIdentifier {
        public string GetDeviceId() {
            return Build.Serial;
        }

        public Task<ILocation> GetDeviceLocationAsync() =>
            Task<ILocation>.Run(async () => {
                ILocation result = null;

                try {
                    GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Low);
                    request.Timeout = TimeSpan.FromSeconds(9);
                    Location location = await Geolocation.GetLocationAsync(request);

                    if (location != null) {
                        result = new Drive.Client.Droid.Models.Services.DeviceIdentifier.Location {
                            Longitude = location.Longitude,
                            Latitude = location.Latitude,
                            TimestampUtc = location.TimestampUtc.UtcDateTime
                        };
                    }
                }
                catch (FeatureNotSupportedException fnsEx) {
                    // TODO: Handle not supported on device exception
                    Debugger.Break();
                }
                catch (PermissionException pEx) {
                    // TODO: Handle permission exception
                    Debugger.Break();
                }
                catch (Exception ex) {
                    // TODO: Unable to get location
                    Debugger.Break();
                }

                return result;
            });
    }
}