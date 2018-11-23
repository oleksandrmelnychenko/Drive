using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CoreFoundation;
using Drive.Client.Helpers;
using Drive.Client.iOS.Models.Notifications;
using Drive.Client.iOS.Services;
using FFImageLoading.Forms.Platform;
using Foundation;
using ObjCRuntime;
using UIKit;
using UserNotifications;
using WindowsAzure.Messaging;
using Xamarin.Forms;

namespace Drive.Client.iOS {
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate {

        private SBNotificationHub Hub { get; set; }

        //public const string ConnectionString = "Endpoint=sb://driveapp.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=Njb19Byfsgn92XST4eCtAkZLJL7JCzOXgDDkWYXybrk=";
        public const string ConnectionString = "Endpoint=sb://driveapp.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=8UWFMZOxaoQS8GDsFcIfSqMZ6KJU1DzrFDGdELF5BN8=";

        public const string NotificationHubPath = "drivenotificationhub";

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

            UIApplication.SharedApplication.RegisterForRemoteNotifications();

            LoadApplication(new App());

            // Request notification permissions from the user
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) => {
                // Handle approval
            });

            // Watch for notifications while the app is active
            UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();

            return base.FinishedLaunching(app, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken) {
            DEVICE_TOKEN = deviceToken.ToString();

            BaseSingleton<GlobalSetting>.Instance.MessagingDeviceToken = DEVICE_TOKEN;
            MessagingCenter.Send(this, "device_token");

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
    }
}
