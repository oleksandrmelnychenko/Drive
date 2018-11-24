using Drive.Client.Models.Notifications;
using Newtonsoft.Json;

namespace Drive.Client.iOS.Models.Notifications {
    public class NotificationMessage : INotificationMessage {
        [JsonProperty("notificationType")]
        public NotificationCaseType NotificationType { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("userNetId")]
        public string UserNetId { get; set; }

        [JsonProperty("alert")]
        public string Alert { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}