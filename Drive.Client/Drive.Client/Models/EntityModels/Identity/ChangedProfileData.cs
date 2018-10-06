using Newtonsoft.Json;
using System;

namespace Drive.Client.Models.EntityModels.Identity {
    public class ChangedProfileData {
        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("NetId")]
        public Guid NetId { get; set; }

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }
    }
}
