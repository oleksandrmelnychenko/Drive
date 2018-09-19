using Drive.Client.Exceptions;
using Drive.Client.Helpers;
using Drive.Client.Models.EntityModels;
using Drive.Client.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.CarsInfo {
    public class CarInfoService : ICarInfoService {

        private readonly IRequestProvider _requestProvider;

        public CarInfoService(IRequestProvider requestProvider) {
            _requestProvider = requestProvider;
        }

        public async Task<List<CarInfo>> GetCarsInfoByCarIdAsync(string carId, CancellationTokenSource cancellationTokenSource = null) =>
            await Task.Run(async () => {
                List<CarInfo> carInfos = null;

                string url = string.Format(BaseSingleton<GlobalSetting>.Instance.RestEndpoints.CarInfoEndPoints.AutoCompleteEndpoint, carId);

                try {
                    carInfos = await _requestProvider.GetAsync<List<CarInfo>>(url);
                }
                catch (ConnectivityException exc) {
                    throw exc;
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR: {ex.Message}");
                    throw new Exception(ex.Message);
                }
                return carInfos;
            });
    }
}
