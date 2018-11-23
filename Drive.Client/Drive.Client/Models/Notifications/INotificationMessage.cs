namespace Drive.Client.Models.Notifications {
    public interface INotificationMessage {

        /// <summary>
        /// Notification short description
        /// </summary>
        NotificationCaseType NotificationType { get; set; }

        /// <summary>
        /// Vehicle request id etc... (depends on notification type)
        /// </summary>
        string Data { get; set; }

        /// <summary>
        /// User who should resolve this notification
        /// </summary>
        string UserNetId { get; set; }
    }
}
