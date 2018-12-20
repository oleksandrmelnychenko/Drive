using System.Threading.Tasks;

namespace Drive.Client.Services.Signal {
    public interface ISignalService {
        Task StartAsync(string accessToken);

        Task StopAsync();
    }
}
