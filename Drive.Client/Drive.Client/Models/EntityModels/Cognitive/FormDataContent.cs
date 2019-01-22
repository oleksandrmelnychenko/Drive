using Drive.Client.Models.Medias;
using System.Collections.Generic;

namespace Drive.Client.Models.EntityModels.Cognitive {
    public class FormDataContent {
        public List<string> Content { get; set; }

        public MediaBase MediaContent { get; set; }
    }
}
