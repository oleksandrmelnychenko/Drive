using Drive.Client.Helpers;
using Drive.Client.Helpers.Localize;
using Drive.Client.Services.Navigation;
using Drive.Client.ViewModels.Base;
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
            INavigationService navigationService = DependencyLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override async void OnStart() {
            base.OnStart();

            await InitNavigation();

            base.OnResume();
        }

        protected override void OnSleep() {
            BaseSingleton<GlobalSetting>.Instance.SaveState();
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
