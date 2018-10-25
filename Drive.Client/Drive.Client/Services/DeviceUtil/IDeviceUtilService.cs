using Drive.Client.Models.Identities.Device;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Drive.Client.Services.DeviceUtil {
    public interface IDeviceUtilService {

        Task<Location> GetDeviceLocationAsync(GeolocationAccuracy accuracy, TimeSpan timeout, CancellationTokenSource cancellationTokenSource);

        Task<ClientHardware> GetDeviceInfoAsync(CancellationTokenSource cancellationTokenSource);

        Task<bool> RegisterClientDeviceInfoAsync(ClientHardware clientHardware, CancellationTokenSource cancellationTokenSource);
    }
}
