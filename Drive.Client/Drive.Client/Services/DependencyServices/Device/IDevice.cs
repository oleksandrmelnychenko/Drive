using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Drive.Client.Services.DependencyServices.Device {
    public interface IDevice {

        //string GetDeviceId();

        Task<Location> GetDeviceLocationAsync();
    }
}
