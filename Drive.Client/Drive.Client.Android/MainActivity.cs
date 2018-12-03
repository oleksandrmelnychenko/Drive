using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Drive.Client.Droid.Models.Notiifactions;
using Drive.Client.Models.Notifications;
using Drive.Client.Views;
using FFImageLoading.Forms.Platform;
using Plugin.CurrentActivity;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Drive.Client.Droid {
    [Activity(MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.Locale | ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {

        private static readonly string RECREIVED_PARSED_RESIDENT_VEHICLE_DETAIL_NOTIFICATION_ENTITY_STRING_EXTRA_KEY = "recreived_parsed_resident_vehicle_detail_notification_entity_string_extra_key";

        internal static MainActivity Instance { get; private set; }

        public static Intent GetIntentWithParsedResidentVehicleDetail(Context context, string jsonNotificationMessage) {
            Intent intent = new Intent(context, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);
            intent.SetAction(Guid.NewGuid().ToString());
            intent.PutExtra(RECREIVED_PARSED_RESIDENT_VEHICLE_DETAIL_NOTIFICATION_ENTITY_STRING_EXTRA_KEY, jsonNotificationMessage);

            return intent;
        }

        protected override void OnCreate(Bundle savedInstanceState) {
            MessagingCenter.Subscribe<object>(this, CustomNavigationView.ON_CUSTOM_NAVIGATION_VIEW_APPEARING, (param) => {
                MessagingCenter.Unsubscribe<object>(this, CustomNavigationView.ON_CUSTOM_NAVIGATION_VIEW_APPEARING);

                Window.SetBackgroundDrawableResource(Resource.Drawable.common_window_background_layer_list_drawable);
            });

            //while (true) {
            //    try {
            //        Firebase.FirebaseApp.InitializeApp(this);
            //        Firebase.FirebaseApp fireApp = Firebase.FirebaseApp.Instance;

            //        break;
            //    }
            //    catch (Exception exc) {
            //        Console.WriteLine(exc.Message);
            //    }
            //}

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CachedImageRenderer.Init(true);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            base.OnCreate(savedInstanceState);

            Instance = this;
            UserDialogs.Init(this);

            LoadApplication(new App());

            IsPlayServicesAvailable();

            //string refreshedToken = FirebaseInstanceId.Instance.Token;
            //Toast.MakeText(this, string.Format("OnCreate: {0}", refreshedToken), ToastLength.Long).Show();

            if (Intent.Extras != null && Intent.Extras.ContainsKey(RECREIVED_PARSED_RESIDENT_VEHICLE_DETAIL_NOTIFICATION_ENTITY_STRING_EXTRA_KEY)) {
                try {
                    string jsonNotificationMessage = Intent.Extras.GetString(RECREIVED_PARSED_RESIDENT_VEHICLE_DETAIL_NOTIFICATION_ENTITY_STRING_EXTRA_KEY);
                    INotificationMessage notificationMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<NotificationMessage>(jsonNotificationMessage);

                    MessagingCenter.Send<object, INotificationMessage>(this, Drive.Client.Services.Notifications.NotificationService.RESIDENT_VEHICLE_DETAIL_RECEIVED_NOTIFICATION, notificationMessage);
                }
                catch (Exception exc) {
                    string message = exc.Message;
                    Debugger.Break();
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults) {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Intent intent) {
            base.OnNewIntent(intent);

            if (intent.Extras != null && intent.Extras.ContainsKey(RECREIVED_PARSED_RESIDENT_VEHICLE_DETAIL_NOTIFICATION_ENTITY_STRING_EXTRA_KEY)) {
                try {
                    string jsonNotificationMessage = intent.Extras.GetString(RECREIVED_PARSED_RESIDENT_VEHICLE_DETAIL_NOTIFICATION_ENTITY_STRING_EXTRA_KEY);
                    INotificationMessage notificationMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<NotificationMessage>(jsonNotificationMessage);

                    MessagingCenter.Send<object, INotificationMessage>(this, Drive.Client.Services.Notifications.NotificationService.RESIDENT_VEHICLE_DETAIL_RECEIVED_NOTIFICATION, notificationMessage);
                }
                catch (Exception exc) {
                    string message = exc.Message;
                    Debugger.Break();
                }
            }
        }

        /// <summary>
        /// Ckecks is google play services available
        /// </summary>
        private bool IsPlayServicesAvailable() {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success) {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode)) {
                    // In a real project you can give the user a chance to fix the issue.
                    Console.WriteLine($"Error: {GoogleApiAvailability.Instance.GetErrorString(resultCode)}");
                }
                else {
                    Console.WriteLine("Error: Play services not supported!");
                    Debugger.Break();
                }

                Toast.MakeText(this, string.Format("Error: Play services not supported!"), ToastLength.Long);
                return false;
            }
            else {
                Console.WriteLine("Play Services available.");
                return true;
            }
        }
    }
}