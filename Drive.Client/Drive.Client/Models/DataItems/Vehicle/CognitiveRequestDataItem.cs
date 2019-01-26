using Drive.Client.Models.EntityModels.Search;
using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.Models.DataItems.Vehicle {
    public class CognitiveRequestDataItem : BaseRequestDataItem {
        CognitiveRequest _cognitiveRequest;
        public CognitiveRequest CognitiveRequest {
            get { return _cognitiveRequest; }
            set { SetProperty(ref _cognitiveRequest, value); }
        }

       
    }
}
