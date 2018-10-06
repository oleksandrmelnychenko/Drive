using Newtonsoft.Json;

namespace Drive.Client.Models.Arguments.IdentityAccounting.Registration {
    public class SignInArgs {
        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }
    }
}
