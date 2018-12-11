using Drive.Client.Models.EntityModels.Announcement;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.Announcement {
    public interface IAnnouncementService {

        Task NewAnnouncementAsync(AnnounceBody announce, CancellationTokenSource cancellationTokenSource);

        Task AskToGetAnnouncementAsync(CancellationTokenSource cancellationTokenSource);
    }
}
