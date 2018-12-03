using Drive.Client.Helpers.Localize;
using Drive.Client.Models.DataItems.Vehicle;
using Drive.Client.Models.EntityModels.Search;
using System.Collections.Generic;

namespace Drive.Client.Factories.Vehicle {
    public interface IVehicleFactory {
        List<BaseRequestDataItem> BuildPolandRequestItems(IEnumerable<PolandVehicleRequest> residentRequests, ResourceLoader resourceLoader);

        List<BaseRequestDataItem> BuildResidentRequestItems(IEnumerable<ResidentRequest> residentRequests, ResourceLoader resourceLoader);
    }
}
