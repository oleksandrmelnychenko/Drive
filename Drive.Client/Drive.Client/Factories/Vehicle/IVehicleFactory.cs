using Drive.Client.Models.DataItems.Vehicle;
using Drive.Client.Models.EntityModels.Search;
using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.Factories.Vehicle {
    public interface IVehicleFactory {
        List<BaseRequestDataItem> BuildPolandRequestItems(IEnumerable<PolandVehicleRequest> residentRequests);

        List<BaseRequestDataItem> BuildResidentRequestItems(IEnumerable<ResidentRequest> residentRequests);
    }
}
