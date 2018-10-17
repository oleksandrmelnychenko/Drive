using Drive.Client.Models.EntityModels.Search;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.Vehicle {
    public interface IVehicleService {
        Task<List<ResidentRequest>> GetUserVehicleDetailRequestsAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
