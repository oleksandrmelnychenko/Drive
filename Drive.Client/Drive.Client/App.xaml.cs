using Drive.Client.Helpers;
using Drive.Client.Helpers.Localize;
using Drive.Client.Models.Notifications;
using Drive.Client.Services.DependencyServices.AppVersion;
using Drive.Client.Services.Navigation;
using Drive.Client.Services.Notifications;
using Drive.Client.ViewModels.Base;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
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

#if DEBUG
            //TrackMemoryUsage();
#endif

            MessagingCenter.Subscribe<object, INotificationMessage>(this, NotificationService.RESIDENT_VEHICLE_DETAIL_RECEIVED_NOTIFICATION, (sender, args) => {
                DependencyLocator.Resolve<INotificationService>().InvokeReceivedResidentVehicleDetailInfo(args);
            });
        }

        private void TrackMemoryUsage() {
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android) {
                Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(5), () => {
                    MemoryHelper.DisplayAndroidMemory();

                    return true;
                });
            }
        }

        private void InitApp() {
            DependencyLocator.RegisterDependencies();
            //ResourceLoader.Init();
        }

        private Task InitNavigation() {
            INavigationService navigationService = DependencyLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override async void OnStart() {
            base.OnStart();

            AppCenter.Start(string.Format("android={0};", BaseSingleton<GlobalSetting>.Instance.AzureMobileCenter.AndroidAppSecret),
                typeof(Analytics), typeof(Crashes));
            AppCenter.LogLevel = LogLevel.Verbose;

            await InitNavigation();
        }

        protected override void OnSleep() {
            base.OnSleep();

            BaseSingleton<GlobalSetting>.Instance.SaveState();
            MessagingCenter.Unsubscribe<object, INotificationMessage>(this, NotificationService.RESIDENT_VEHICLE_DETAIL_RECEIVED_NOTIFICATION);

            DependencyLocator.Resolve<INavigationService>().UnsubscribeAfterSleepApp();
        }

        protected override void OnResume() {
            // Handle when your app resumes
            base.OnResume();

            DependencyLocator.Resolve<INavigationService>().InitAfterResumeApp();
        }
    }
}
