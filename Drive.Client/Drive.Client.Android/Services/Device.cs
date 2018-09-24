using Android.OS;
using Drive.Client.Services.DependencyServices.Device;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(Drive.Client.Droid.Services.Device))]
namespace Drive.Client.Droid.Services {
    internal class Device : IDevice {
        //public string GetDeviceId() {
        //    return Build.Serial;
        //}

        public Task<Location> GetDeviceLocationAsync() =>
            Task<Location>.Run(async () => {
                Location result = null;

                try {
                    GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Low);
                    request.Timeout = TimeSpan.FromSeconds(9);
                    result = await Geolocation.GetLocationAsync(request);
                }
                catch (FeatureNotSupportedException fnsEx) {
                    // TODO: Handle not supported on device exception
                    System.Diagnostics.Debug.WriteLine($"---ERROR: {fnsEx.Message}");
                    Debugger.Break();
                }
                catch (PermissionException pEx) {
                    // TODO: Handle permission exception
                    System.Diagnostics.Debug.WriteLine($"---ERROR: {pEx.Message}");
                    Debugger.Break();
                }
                catch (Exception ex) {
                    // TODO: Unable to get location
                    System.Diagnostics.Debug.WriteLine($"---ERROR: {ex.Message}");
                    Debugger.Break();
                }

                return result;
            });
    }
}