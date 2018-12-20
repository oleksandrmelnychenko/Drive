using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AudioToolbox;
using CoreFoundation;
using Drive.Client.Helpers;
using Drive.Client.iOS.Models.Notifications;
using Drive.Client.iOS.Services;
using FFImageLoading.Forms.Platform;
using Foundation;
using KeyboardOverlap.Forms.Plugin.iOSUnified;
using ObjCRuntime;
using PushKit;
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

        private UserNotificationCenterDelegate _userNotificationCenterDelegate;

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
            //KeyboardOverlapRenderer.Init();

            UIApplication.SharedApplication.RegisterForRemoteNotifications();

            ConfigereNotification();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void ConfigereNotification() {
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0)) {
                //Request notification permissions from the user
                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {
                    Console.WriteLine(granted);
                });

                //Watch for notifications while the app is active
                UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();
            } else {
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken) {
            string originalDeviceToken = deviceToken.ToString();
            DEVICE_TOKEN = originalDeviceToken.Trim('<').Trim('>').Replace(" ", "");

            BaseSingleton<GlobalSetting>.Instance.MessagingDeviceToken = DEVICE_TOKEN;
            MessagingCenter.Send<object>(this, "device_token");

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
