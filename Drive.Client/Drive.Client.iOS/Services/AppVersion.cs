using Drive.Client.iOS.Services;
using Drive.Client.Services.DependencyServices.AppVersion;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppVersion))]
namespace Drive.Client.iOS.Services {
    internal class AppVersion : IAppVersion {
        public string GetBuild() {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString();
        }

        public string GetVersion() {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }
    }
}