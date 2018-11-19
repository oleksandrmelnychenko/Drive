
using Android.App;
using Android.Content;
using Android.Widget;
using Drive.Client.Helpers;
using Firebase.Iid;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Drive.Client.Droid.Controllers.Services {
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class DriveFirebaseIIDService : FirebaseInstanceIdService {

        public override void OnTokenRefresh() {
            //base.OnTokenRefresh();
            try {
                string deviceToken = FirebaseInstanceId.Instance.Token;
                BaseSingleton<GlobalSetting>.Instance.MessagingDeviceToken = deviceToken;

                MessagingCenter.Send<object>(this, "device_token");
            } catch (Exception ex) {
                Debug.WriteLine($"ERRROR: {ex.Message}");
            }
        }
    }
}