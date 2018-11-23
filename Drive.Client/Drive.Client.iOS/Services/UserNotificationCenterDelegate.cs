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

namespace Drive.Client.iOS.Services {
    internal class UserNotificationCenterDelegate : UNUserNotificationCenterDelegate {

        public UserNotificationCenterDelegate() {
        }

        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler) {
            try {
                NSDictionary aps = response.Notification.Request.Content.UserInfo.ObjectForKey(new NSString("aps")) as NSDictionary;
                NSData jsonData = NSJsonSerialization.Serialize(aps, 0, out NSError error);
                string jsonNotificationMessage = jsonData.ToString();

                NotificationMessage notificationMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<NotificationMessage>(jsonNotificationMessage);

                MessagingCenter.Send<object, INotificationMessage>(this, Drive.Client.Services.Notifications.NotificationService.RESIDENT_VEHICLE_DETAIL_RECEIVED_NOTIFICATION, notificationMessage);
            } catch (Exception exc) {
                string message = exc.Message;
                Debugger.Break();
            }
        }

        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler) {
            try {
                NSDictionary aps = notification.Request.Content.UserInfo.ObjectForKey(new NSString("aps")) as NSDictionary;

                if (aps == null) return;

                NSData jsonData = NSJsonSerialization.Serialize(aps, 0, out NSError error);
                string jsonResult = jsonData.ToString();

                NotificationMessage notificationMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<NotificationMessage>(jsonResult);
                //BaseSingleton<GlobalSetting>.Instance.UserProfile?.NetId == notificationMessage.UserNetId

                // Rebuild notification
                var content = new UNMutableNotificationContent {
                    Title = notificationMessage.Title,
                    Subtitle = notificationMessage.Alert,
                    Body = notificationMessage.Data                    
                };

                // New trigger time
                var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(5, false);

                // ID of Notification to be updated
                var requestID = notification.Request.Identifier;
                var request = UNNotificationRequest.FromIdentifier(requestID, content, trigger);

                // Add to system to modify existing Notification
                UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) => {
                    if (err != null) {
                        // Do something with error...
                        Debugger.Break();
                    }
                });

                // Tell system to display the notification anyway or use
                // `None` to say we have handled the display locally.
                completionHandler(UNNotificationPresentationOptions.Alert);
            } catch (Exception ex) {
                string message = ex.Message;
                Debugger.Break();
            }
        }
    }
}