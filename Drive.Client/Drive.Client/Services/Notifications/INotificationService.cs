using Drive.Client.Models.Arguments.Notifications;
using System;

namespace Drive.Client.Services.Notifications {
    public interface INotificationService {

        event EventHandler<ReceivedResidentVehicleDetailInfoArgs> ReceivedResidentVehicleDetailInfo;
    }
}
