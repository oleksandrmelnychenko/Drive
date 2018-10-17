using Drive.Client.Exceptions;
using Drive.Client.Helpers;
using Drive.Client.Models.EntityModels;
using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.Automobile {
    public class DriveAutoService : IDriveAutoService {

        private readonly IRequestProvider _requestProvider;

        /// <summary>
        ///     ctor().
        /// </summary>
        /// <param name="requestProvider"></param>
        public DriveAutoService(IRequestProvider requestProvider) {
            _requestProvider = requestProvider;
        }

        /// <summary>
        /// Get all drive autos.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cancellationTokenSource"></param>
        /// <returns></returns>
        public async Task<List<DriveAuto>> GetAllDriveAutosAsync(string value, CancellationTokenSource cancellationTokenSource = null) =>
             await Task.Run(async () => {
                 List<DriveAuto> driveAutos = null;

                 string url = string.Format(BaseSingleton<GlobalSetting>.Instance.RestEndpoints.CarInfoEndPoints.GetAllCarInfoEndPoint, value);

                 try {
                     driveAutos = await _requestProvider.GetAsync<List<DriveAuto>>(url);
                 }
                 catch (ConnectivityException exc) {
                     throw exc;
                 }
                 catch (Exception ex) {
                     Debug.WriteLine($"ERROR: {ex.Message}");
                     throw new Exception(ex.Message);
                 }
                 return driveAutos;
             }, cancellationTokenSource.Token);

        /// <summary>
        /// Get car numbers autocomplete.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cancellationTokenSource"></param>
        /// <returns></returns>
        public async Task<List<DriveAutoSearch>> GetCarNumbersAutocompleteAsync(string value, CancellationTokenSource cancellationTokenSource = null) =>
            await Task.Run(async () => {
                List<DriveAutoSearch> carNumbers = null;

                string url = string.Format(BaseSingleton<GlobalSetting>.Instance.RestEndpoints.CarInfoEndPoints.AutoCompleteEndpoint, value);

                try {
                    carNumbers = await _requestProvider.GetAsync<List<DriveAutoSearch>>(url);
                }
                catch (ConnectivityException exc) {
                    throw exc;
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR: {ex.Message}");
                    throw new Exception(ex.Message);
                }
                return carNumbers;
            }, cancellationTokenSource.Token);
        
        /// <summary>
        /// Get drive auto by full match number.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="cancellationTokenSource"></param>
        /// <returns></returns>
        public async Task<List<DriveAuto>> GetDriveAutoByNumberAsync(string number, CancellationTokenSource cancellationTokenSource = null) =>
            await Task.Run(async () => {
                List<DriveAuto> carInfos = null;

                string url = string.Format(BaseSingleton<GlobalSetting>.Instance.RestEndpoints.CarInfoEndPoints.GetByNumberEndPoint, number);

                try {
                    carInfos = await _requestProvider.GetAsync<List<DriveAuto>>(url);
                }
                catch (ConnectivityException exc) {
                    throw exc;
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR: {ex.Message}");
                    throw new Exception(ex.Message);
                }
                return carInfos;
            }, cancellationTokenSource.Token);
    }
}
