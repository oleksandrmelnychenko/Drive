using Drive.Client.Models.EntityModels.Announcement;
using System;

namespace Drive.Client.Services.Signal.Announcement {
    public interface IAnnouncementSignalService : ISignalService {

        event EventHandler<Announce> NewAnnounceReceived;

        event EventHandler<Announce[]> GetAnnouncement;
    }
}
