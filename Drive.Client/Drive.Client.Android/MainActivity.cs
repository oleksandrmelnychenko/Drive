using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Drive.Client.Views;
using FFImageLoading.Forms.Platform;
using Plugin.CurrentActivity;
using Xamarin.Forms;

namespace Drive.Client.Droid {
    [Activity( MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {

        internal static MainActivity Instance { get; private set; }
       
        protected override void OnCreate(Bundle savedInstanceState) {
            MessagingCenter.Subscribe<object>(this, CustomNavigationView.ON_CUSTOM_NAVIGATION_VIEW_APPEARING, (param) => {
                MessagingCenter.Unsubscribe<object>(this, CustomNavigationView.ON_CUSTOM_NAVIGATION_VIEW_APPEARING);

                Window.SetBackgroundDrawableResource(Resource.Drawable.common_window_background_layer_list_drawable);
            });

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
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults) {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}