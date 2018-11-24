using Drive.Client.Helpers;
using Drive.Client.Models.Arguments.Notifications;
using Drive.Client.Models.Notifications;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Drive.Client.Services.Notifications {
    public class NotificationService : INotificationService {

        public static string RESIDENT_VEHICLE_DETAIL_RECEIVED_NOTIFICATION = "NotificationService.resident_vehicle_detail_received_notification";

        public event EventHandler<ReceivedResidentVehicleDetailInfoArgs> ReceivedResidentVehicleDetailInfo = delegate { };

        public NotificationService() {
            MessagingCenter.Subscribe<object, INotificationMessage>(this, RESIDENT_VEHICLE_DETAIL_RECEIVED_NOTIFICATION, (sender, args) => {
                try {
                    Analytics.TrackEvent("NotificationService.MessagingCenter.Subscribe<object, INotificationMessage>", new Dictionary<string, string> { { "data", args.Data} });

                    if (args?.UserNetId == BaseSingleton<GlobalSetting>.Instance.UserProfile.NetId) {
                        ReceivedResidentVehicleDetailInfo(this, new ReceivedResidentVehicleDetailInfoArgs() { RecidentVehicleNotification = args });
                    }
                }
                catch (Exception exc) {
                    Crashes.TrackError(exc);
                }
            });
        }

        ~NotificationService() {

        }
    }
}
