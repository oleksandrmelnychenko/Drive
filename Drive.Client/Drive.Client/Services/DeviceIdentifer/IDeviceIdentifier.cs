using Drive.Client.Models.Services.DeviceIdentifier;
using System.Threading.Tasks;

namespace Drive.Client.Services.DeviceIdentifer {
    public interface IDeviceIdentifier {
        string GetDeviceId();

        Task<ILocation> GetDeviceLocationAsync();
    }
}
