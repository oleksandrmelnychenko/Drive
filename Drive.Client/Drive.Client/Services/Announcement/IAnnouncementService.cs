using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.Models.Rest;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.Announcement {
    public interface IAnnouncementService {
        Task AskToGetAnnouncementAsync(CancellationTokenSource cancellationTokenSource);

        Task NewAnnouncementAsync(AnnounceBodyRequest announceBodyRequest, CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource), string eventId = "");

        Task<string> UploadAttachedDataAsync(IEnumerable<AttachedAnnounceMediaBase> attachedData, CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource));
    }
}
