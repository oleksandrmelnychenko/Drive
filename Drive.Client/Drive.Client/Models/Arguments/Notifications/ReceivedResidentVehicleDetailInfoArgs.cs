using Drive.Client.Models.Notifications;
using System;

namespace Drive.Client.Models.Arguments.Notifications {
    public class ReceivedResidentVehicleDetailInfoArgs : EventArgs {

        public INotificationMessage RecidentVehicleNotification { get; set; }
    }
}
