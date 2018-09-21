using Drive.Client.iOS.Services;
using Drive.Client.Services.DeviceIdentifer;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceIdReceiver))]
namespace Drive.Client.iOS.Services {
    internal class DeviceIdReceiver : IDeviceIdentifier {
        public string GetDeviceId() {
            return UIDevice.CurrentDevice.IdentifierForVendor.AsString();
        }
    }
}