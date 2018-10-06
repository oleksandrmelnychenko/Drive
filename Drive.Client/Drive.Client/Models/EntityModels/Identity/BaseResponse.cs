using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.Models.EntityModels.Identity {
    public abstract class BaseResponse {
        [JsonProperty("IsRequestSuccess")]
        public bool IsRequestSuccess { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Data")]
        public object Data { get; set; }
    }
}
