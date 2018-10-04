using Newtonsoft.Json;

namespace Drive.Client.Models.EntityModels.Identity {
    public class User {
        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("NetId")]
        public string NetId { get; set; }

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }
    }
}
