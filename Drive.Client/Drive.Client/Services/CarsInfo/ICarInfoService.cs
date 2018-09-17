using Drive.Client.Models.EntityModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.CarsInfo {
    public interface ICarInfoService {
        Task<List<CarInfo>> GetCarsInfoByCarIdAsync(string carId, CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource));
    }
}
