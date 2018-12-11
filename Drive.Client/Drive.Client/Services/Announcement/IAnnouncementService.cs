using Drive.Client.Models.EntityModels.Announcement;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.Announcement {
    public interface IAnnouncementService {

        Task<bool> NewAnnouncementAsync(TODOAnnounce announce, CancellationTokenSource cancellationTokenSource);
    }
}
