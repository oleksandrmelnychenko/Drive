
using Android.App;
using Android.Content;
using Android.Widget;
using Firebase.Iid;
using System;

namespace Drive.Client.Droid.Controllers.Services {
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class DriveFirebaseIIDService : FirebaseInstanceIdService {

        public override void OnTokenRefresh() {
            //base.OnTokenRefresh();

            string refreshedToken = FirebaseInstanceId.Instance.Token;
            Console.WriteLine("Token received {0}", refreshedToken);
        }
    }
}