using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserNotifications;
using Foundation;
using UIKit;
using System.Diagnostics;
using Drive.Client.iOS.Models.Notifications;
using Drive.Client.Helpers;
using Xamarin.Forms;
using Drive.Client.Models.Notifications;
using AudioToolbox;

namespace Drive.Client.iOS.Services {
    internal class UserNotificationCenterDelegate : UNUserNotificationCenterDelegate {

        private const string REBUILD_NOTIFICATION_KEY = "rebuildedID";

        /// <summary>
        ///     ctor().
        /// </summary>
        public UserNotificationCenterDelegate() { }

        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler) {
            try {
                NSDictionary aps = response.Notification.Request.Content.UserInfo.ObjectForKey(new NSString("aps")) as NSDictionary;
                NSData jsonData = NSJsonSerialization.Serialize(aps, 0, out NSError error);
                string jsonNotificationMessage = jsonData.ToString();

                bool forMe = default(bool);

                NotificationMessage notificationMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<NotificationMessage>(jsonNotificationMessage);
                forMe = BaseSingleton<GlobalSetting>.Instance.UserProfile?.NetId == notificationMessage.UserNetId;
                if (forMe) {
                    MessagingCenter.Send<object, INotificationMessage>(this, Drive.Client.Services.Notifications.NotificationService.RESIDENT_VEHICLE_DETAIL_RECEIVED_NOTIFICATION, notificationMessage);
                }
            } catch (Exception exc) {
                string message = exc.Message;
                Debugger.Break();
            }
        }

        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler) {
            try {
                if (!(notification.Request.Content.UserInfo.ObjectForKey(new NSString("aps")) is NSDictionary aps)) return;

                if (notification.Request.Identifier == REBUILD_NOTIFICATION_KEY) {
                    completionHandler(UNNotificationPresentationOptions.Sound | UNNotificationPresentationOptions.Alert);
                } else {
                    NSData jsonData = NSJsonSerialization.Serialize(aps, 0, out NSError error);
                    string jsonResult = jsonData.ToString();
                    bool forMe = default(bool);

                    NotificationMessage notificationMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<NotificationMessage>(jsonResult);
                    forMe = BaseSingleton<GlobalSetting>.Instance.UserProfile?.NetId == notificationMessage.UserNetId;

                    if (forMe) {
                        // Rebuild notification
                        var content = new UNMutableNotificationContent {
                            Sound = UNNotificationSound.Default,
                            Title = "Запит по фізичній особі",
                            Body = "Оброблено",
                            UserInfo = notification.Request.Content.UserInfo
                        };

                        // New trigger time
                        var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(1, false);

                        // ID of Notification to be updated
                        var requestID = REBUILD_NOTIFICATION_KEY;
                        var request = UNNotificationRequest.FromIdentifier(requestID, content, trigger);

                        // Add to system to modify existing Notification
                        UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) => {
                            if (err != null) {
                                // Do something with error...
                                Debugger.Break();
                            }
                        });
                    }
                }
            } catch (Exception ex) {
                string message = ex.Message;
                Debugger.Break();
            }
            //completionHandler(UNNotificationPresentationOptions.None);
        }
    }
}