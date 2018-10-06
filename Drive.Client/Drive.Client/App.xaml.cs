using Drive.Client.Helpers;
using Drive.Client.Helpers.Localize;
using Drive.Client.Services.Navigation;
using Drive.Client.ViewModels.Base;
using Newtonsoft.Json;
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
            ResourceLoader.Init();
        }

        private Task InitNavigation() {
            //string accesToken = (JsonConvert.DeserializeObject<UserProfile>(Settings.UserProfile))?.AccesToken;
            INavigationService navigationService = DependencyLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
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
