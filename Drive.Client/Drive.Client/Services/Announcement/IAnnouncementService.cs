using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.Models.Rest;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.Announcement {
    public interface IAnnouncementService {
        Task SetLikeStatusAsync(string postId, CancellationTokenSource cancellationTokenSource);

        Task NewAnnouncementAsync(AnnounceBodyRequest announceBodyRequest, CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource), string eventId = "");

        Task<string> UploadAttachedDataAsync(IEnumerable<AttachedAnnounceMediaBase> attachedData, CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource));

        Task DeleteAnnounceAsync(string id, CancellationTokenSource cancellationTokenSource);

        Task<List<Announce>> GetAnnouncesAsync(CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource));
    }
}
