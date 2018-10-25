using Android.Content.PM;
using Drive.Client.Droid.Services;
using Drive.Client.Services.DependencyServices.AppVersion;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppVersion))]
namespace Drive.Client.Droid.Services {
    internal class AppVersion : IAppVersion {
        public string GetVersion() {
            var context = global::Android.App.Application.Context;

            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionName;
        }

        public string GetBuild() {
            var context = global::Android.App.Application.Context;
            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionCode.ToString();
        }
    }
}