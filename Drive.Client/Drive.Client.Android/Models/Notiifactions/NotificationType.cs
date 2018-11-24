using Drive.Client.Models.Notifications;
using Newtonsoft.Json;

namespace Drive.Client.Droid.Models.Notiifactions {
    public class NotificationType {

        [JsonProperty("Case")]
        public NotificationCaseType Case { get; set; }
    }
}