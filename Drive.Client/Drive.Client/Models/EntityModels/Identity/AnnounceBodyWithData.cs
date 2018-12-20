using Drive.Client.Models.EntityModels.Announcement;
using System.Collections.Generic;

namespace Drive.Client.Models.EntityModels.Identity {
    internal class AnnounceBodyWithData {
        public AnnounceBodyRequest AnnounceBodyRequest { get; set; }

        public IEnumerable<AttachedAnnounceMediaBase> AttachedData { get; set; }
    }
}
