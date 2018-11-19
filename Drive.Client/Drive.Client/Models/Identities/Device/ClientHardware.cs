using Newtonsoft.Json;
using Plugin.DeviceInfo.Abstractions;

namespace Drive.Client.Models.Identities.Device {
    public class ClientHardware {
        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }

        [JsonProperty("timestampUtc")]
        public string TimestampUtc { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("deviceName")]
        public string DeviceName { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("versionNumber")]
        public string VersionNumber { get; set; }

        [JsonProperty("appVersion")]
        public string AppVersion { get; set; }

        [JsonProperty("appBuild")]
        public string AppBuild { get; set; }

        [JsonProperty("platform")]
        public Platform Platform { get; set; }

        [JsonProperty("idiom")]
        public Idiom Idiom { get; set; }

        [JsonProperty("isDevice")]
        public bool IsDevice { get; set; }

        [JsonProperty("messagingDeviceToken")]
        public string MessagingDeviceToken { get; set; }
    }
}
