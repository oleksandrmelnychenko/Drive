using Drive.Client.Helpers;
using Drive.Client.Models.Identities;
using Drive.Client.Models.Services.DeviceIdentifier;
using Drive.Client.Services.DeviceIdentifer;
using Drive.Client.Services.Navigation;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views;
using Newtonsoft.Json;
using Plugin.DeviceInfo;
using Plugin.DeviceInfo.Abstractions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Drive.Client {
    public partial class App : Application {

        /// <summary>
        ///     ctor().
        /// </summary>
        public App() {
            InitializeComponent();

            InitApp();
        }

        private void InitApp() {
            DependencyLocator.RegisterDependencies();
        }

        private Task InitNavigation() {
            string accesToken = (JsonConvert.DeserializeObject<UserProfile>(Settings.UserProfile))?.AccesToken;

            INavigationService navigationService = DependencyLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync(string.IsNullOrEmpty(accesToken));
        }

        protected override async void OnStart() {
            base.OnStart();


            TODO();

            await InitNavigation();

            base.OnResume();
        }

        protected override void OnSleep() {
            Settings.UserProfile = JsonConvert.SerializeObject(BaseSingleton<GlobalSetting>.Instance.UserProfile);
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }

        private async void TODO() {
            try {
                ILocation location = await DependencyService.Get<IDeviceIdentifier>().GetDeviceLocationAsync();
                Debugger.Break();

                string deviceId = DependencyService.Get<IDeviceIdentifier>().GetDeviceId();

                string appID = CrossDeviceInfo.Current.GenerateAppId();

                string Id = CrossDeviceInfo.Current.Id;
                string Model = CrossDeviceInfo.Current.Model;
                string Manufacturer = CrossDeviceInfo.Current.Manufacturer;
                string DeviceName = CrossDeviceInfo.Current.DeviceName;
                string Version = CrossDeviceInfo.Current.Version;
                Version VersionNumber = CrossDeviceInfo.Current.VersionNumber;
                string AppVersion = CrossDeviceInfo.Current.AppVersion;
                string AppBuild = CrossDeviceInfo.Current.AppBuild;
                Platform Platform = CrossDeviceInfo.Current.Platform;
                Idiom Idiom = CrossDeviceInfo.Current.Idiom;
                bool IsDevice = CrossDeviceInfo.Current.IsDevice;

                Debugger.Break();
            }
            catch (Exception exc) {

                Debugger.Break();
            }
        }
    }

    //public class DeviceInfo {
    //    double Longitude { get; set; }

    //    double Latitude { get; set; }

    //    DateTime TimestampUtc { get; set; }


    //}
}
