using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Models.EntityModels.Search.Contracts;
using Drive.Client.ViewModels.Base;
using System;

namespace Drive.Client.Models.DataItems.Vehicle {
    public abstract class BaseRequestDataItem : NestedViewModel, IUserVehicleRequest {
        public DateTime Created { get; set; }

        public RequestType HistoryType { get; set; }
    }
}
