using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Firebase.Messaging;
using System;
using System.Diagnostics;

namespace Drive.Client.Droid.Controllers.Services {
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class DriveFirebaseMessagingService : FirebaseMessagingService {

        public override void OnMessageReceived(RemoteMessage message) {
            base.OnMessageReceived(message);

            Console.WriteLine("Received: {0}", message);

            try {
                ///
                /// TODO: handle incomming message parse message and invoke appropriate `shared project service`... 
                /// 
                TestNotificationTemporaryImplementation(message);
            }
            catch (Exception exc) {
                Console.WriteLine("Error occured while receiving push-message. {0}", exc.Message);
                Debugger.Break();
            }
        }

        private void TestNotificationTemporaryImplementation(RemoteMessage message) {
            try {
                NotificationCompat.Builder mBuilder = new Android.Support.V4.App.NotificationCompat.Builder(this)
                    .SetPriority(NotificationCompat.PriorityHigh)
                    .SetVibrate(new long[] { 1000, 2000, 3000 })
                    .SetSmallIcon(Resource.Mipmap.im_logo_d)
                    .SetContentTitle("TODO: content title")
                    .SetContentText("TODO: content text")
                    .SetAutoCancel(true);

                NotificationManager mNotificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
                mNotificationManager.Notify(0, mBuilder.Build());
            }
            catch (Exception exc) {
                Console.WriteLine("Cant build bar notification. {0}", exc.Message);
                Debugger.Break();
            }
        }
    }
}