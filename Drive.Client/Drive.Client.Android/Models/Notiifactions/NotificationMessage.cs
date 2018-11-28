using Drive.Client.Models.Notifications;
using Newtonsoft.Json;

namespace Drive.Client.Droid.Models.Notiifactions {
    public class NotificationMessage : INotificationMessage {

        [JsonProperty("notificationType")]
        public NotificationCaseType NotificationType { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("userNetId")]
        public string UserNetId { get; set; }

        //private NotificationType _caseype;
        //public NotificationType Casetype {
        //    get => _caseype;
        //    set {
        //        _caseype = value;
        //        NotificationType = _caseype != null ? _caseype.Case : NotificationCaseType.ParsedResidentVehicleDetail;
        //    }
        //}
    }
}