using Drive.Client.iOS.Services;
using Drive.Client.Services.DependencyServices.Device;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(Drive.Client.iOS.Services.Device))]
namespace Drive.Client.iOS.Services {
    internal class Device : IDevice {
        //public string GetDeviceId() {
        //    return UIDevice.CurrentDevice.IdentifierForVendor.AsString();
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
                    Debug.WriteLine($"---ERROR: {fnsEx.Message}");
                    Debugger.Break();
                }
                catch (PermissionException pEx) {
                    // TODO: Handle permission exception
                    Debug.WriteLine($"---ERROR: {pEx.Message}");
                    Debugger.Break();
                }
                catch (Exception ex) {
                    // TODO: Unable to get location
                    Debug.WriteLine($"---ERROR: {ex.Message}");
                    Debugger.Break();
                }

                return result;
            });
    }
}