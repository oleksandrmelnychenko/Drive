
using Android.App;
using Android.Content;
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
            base.OnTokenRefresh();

            try {
                string messagingDeviceToken = FirebaseInstanceId.Instance.Token;

                Console.WriteLine("Notification token: {0}", messagingDeviceToken);

                BaseSingleton<GlobalSetting>.Instance.MessagingDeviceToken = messagingDeviceToken;

                MessagingCenter.Send<object>(this, "device_token");
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERRROR: {ex.Message}");
            }
        }
    }
}