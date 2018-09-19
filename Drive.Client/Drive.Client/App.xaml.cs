using Drive.Client.Helpers;
using Drive.Client.Models.Identities;
using Drive.Client.Services.Navigation;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views;
using Newtonsoft.Json;
using System;
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

            await InitNavigation();

            base.OnResume();
        }

        protected override void OnSleep() {
            Settings.UserProfile = JsonConvert.SerializeObject(BaseSingleton<GlobalSetting>.Instance.UserProfile);
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
