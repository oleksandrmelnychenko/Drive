using Drive.Client.Models.Medias;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Drive.Client.Models.EntityModels.Cognitive {
    public class FormDataContent {

        [JsonProperty("Data")]
        public List<string> Content { get; set; }

        [JsonIgnore]
        public MediaBase MediaContent { get; set; }
    }
}
