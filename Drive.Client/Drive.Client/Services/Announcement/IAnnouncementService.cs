﻿using Drive.Client.Models.EntityModels.Announcement;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.Announcement {
    public interface IAnnouncementService {
        Task AskToGetAnnouncementAsync(CancellationTokenSource cancellationTokenSource);

        Task NewAnnouncementAsync(AnnounceBody announce, CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource), string eventId = "");

        Task<string> UploadAttachedDataAsync(IEnumerable<AttachedAnnounceMediaBase> attachedData, CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource));
    }
}
