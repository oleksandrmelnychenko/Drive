using Drive.Client.Models.Notifications;
using Newtonsoft.Json;

namespace Drive.Client.Droid.Models.Notiifactions {
    public class NotificationMessage : INotificationMessage {

        [JsonProperty("notificationType")]
        public INotificationType NotificationType { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("userNetId")]
        public string UserNetId { get; set; }
    }
}