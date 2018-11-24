using Drive.Client.Helpers;
using Drive.Client.Models.Arguments.Notifications;
using Drive.Client.Models.Notifications;
using Microsoft.AppCenter.Crashes;
using System;

namespace Drive.Client.Services.Notifications {
    public class NotificationService : INotificationService {

        public static string RESIDENT_VEHICLE_DETAIL_RECEIVED_NOTIFICATION = "NotificationService.resident_vehicle_detail_received_notification";

        private INotificationMessage _lastReceivedNotificationMessage;

        public NotificationService() {

        }

        public event EventHandler<ReceivedResidentVehicleDetailInfoArgs> ReceivedResidentVehicleDetailInfo = delegate { };

        public void InvokeReceivedResidentVehicleDetailInfo(INotificationMessage notificationMessage) {
            try {
                _lastReceivedNotificationMessage = notificationMessage;

                if (_lastReceivedNotificationMessage != null) {
                    if (_lastReceivedNotificationMessage.UserNetId == BaseSingleton<GlobalSetting>.Instance.UserProfile.NetId) {
                        ReceivedResidentVehicleDetailInfo(this, new ReceivedResidentVehicleDetailInfoArgs() { RecidentVehicleNotification = notificationMessage });
                    }
                }
            }
            catch (Exception exc) {
                Crashes.TrackError(exc);
            }
        }

        public void TryToResolveLastReceivedNotification() => InvokeReceivedResidentVehicleDetailInfo(_lastReceivedNotificationMessage);
    }
}
