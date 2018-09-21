using Android.OS;
using Drive.Client.Droid.Services;
using Drive.Client.Services.DeviceIdentifer;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceIdReceiver))]
namespace Drive.Client.Droid.Services {
    internal class DeviceIdReceiver : IDeviceIdentifier {
        public string GetDeviceId() {
            return Build.Serial;
        }
    }
}