namespace Drive.Client.Models.Notifications {
    public interface INotificationType {

        /// <summary>
        /// Notification case
        /// </summary>
        NotificationCaseType Case { get; set; }
    }
}
