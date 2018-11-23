using Drive.Client.Models.Notifications;
using Newtonsoft.Json;

namespace Drive.Client.Droid.Models.Notiifactions {
    public class NotificationType : INotificationType {

        [JsonProperty("Case")]
        public NotificationCaseType Case { get; set; }
    }
}