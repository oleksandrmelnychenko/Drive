using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FFImageLoading.Forms.Platform;
using Foundation;
using UIKit;
using WindowsAzure.Messaging;

namespace Drive.Client.iOS {
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate {

        private SBNotificationHub Hub { get; set; }

        public const string ConnectionString = "";

        public const string NotificationHubPath = "";

        public static string DEVICE_TOKEN = string.Empty;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {

            global::Xamarin.Forms.Forms.Init();

            CachedImageRenderer.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken) {
            DEVICE_TOKEN = deviceToken.ToString();

            // Create a new notification hub with the connection string and hub path
            Hub = new SBNotificationHub(ConnectionString, NotificationHubPath);

            // Unregister any previous instances using the device token
            Hub.UnregisterAllAsync(deviceToken, (error) => {

                if (error != null) {
                    // Error unregistering
                    return;
                }

                // Register this device with the notification hub
                Hub.RegisterNativeAsync(deviceToken, null, (registerError) => {
                    if (registerError != null) {
                        // Error registering
                    }
                });
            });
        }

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo) {
            // This method is called when a remote notification is received and the
            // App is in the foreground - i.e., not backgrounded

            // We need to check that the notification has a payload (userInfo) and the payload
            // has the root "aps" key in the dictionary - this "aps" dictionary contains defined
            // keys by Apple which allows the system to determine how to handle the alert
            if (null != userInfo && userInfo.ContainsKey(new NSString("aps"))) {
                // Get the aps dictionary from the alert payload
                NSDictionary aps = userInfo.ObjectForKey(new NSString("aps")) as NSDictionary;
                // Here we can do any additional processing upon receiving the noification
                // As the app is in the foreground, we can handle this alert manually
                // here by creating a UIAlert for example
                try {
                    NSData jsonData = NSJsonSerialization.Serialize(aps, 0, out NSError error);
                    string jsonResult = jsonData.ToString();
                    //AppleNotificationData appleNotificationData = Newtonsoft.Json.JsonConvert.DeserializeObject<AppleNotificationData>(jsonResult);

                    //switch (appleNotificationData.NotificationType) {
                    //    case NotificationType.Message:
                    //        MessagingCenter.Send<object, IAppleNotificationData>(this, Constants.IOS_USER_TO_USER, appleNotificationData);
                    //        break;
                    //    case NotificationType.AdminAlert:
                    //        Debugger.Break();
                    //        break;
                    //    case NotificationType.NewPost:
                    //        Debugger.Break();
                    //        break;
                    //    default:
                    //        Debugger.Break();
                    //        break;
                    //}
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR:{ex.Message}");
                }
            }
        }
    }
}
