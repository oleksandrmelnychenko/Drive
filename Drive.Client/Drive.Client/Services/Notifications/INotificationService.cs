using Drive.Client.Models.Arguments.Notifications;
using Drive.Client.Models.Notifications;
using System;

namespace Drive.Client.Services.Notifications {
    public interface INotificationService {

        event EventHandler<ReceivedResidentVehicleDetailInfoArgs> ReceivedResidentVehicleDetailInfo;

        void InvokeReceivedResidentVehicleDetailInfo(INotificationMessage notificationMessage);

        void TryToResolveLastReceivedNotification();
    }
}
