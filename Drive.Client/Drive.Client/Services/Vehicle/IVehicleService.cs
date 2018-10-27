using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Models.EntityModels.Vehicle;
using Drive.Client.Models.Identities.NavigationArgs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.Vehicle {
    public interface IVehicleService {
        Task<List<ResidentRequest>> GetUserVehicleDetailRequestsAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<List<VehicleDetail>> GetVehiclesByRequestIdAsync(long govRequestId, CancellationToken cancellationToken = default(CancellationToken));

        Task<VehicleDetailsByResidentFullName> GetVehicleDetailsByResidentFullNameAsync(SearchByPersonArgs searchByPersonArgs, CancellationToken cancellationToken = default(CancellationToken));
        Task<PolandVehicleDetail> GetPolandVehicleDetails(SearchByPolandNumberArgs searchByPolandNumberArgs, CancellationToken cancellationToken = default(CancellationToken));
    }
}
