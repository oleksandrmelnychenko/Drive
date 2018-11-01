using Drive.Client.Models.EntityModels.Search.Contracts;
using Drive.Client.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.Models.DataItems.Vehicle {
    public abstract class BaseRequestDataItem : NestedViewModel, IUserVehicleRequest {
        public DateTime Created { get; set; }
    }
}
