using Xamarin.Forms;

namespace Drive.Client.Models.EntityModels.Announcement {
    public abstract class AttachedAnnounceMediaBase {

        public string DataBase64 { get; set; }

        public ImageSource MediaPresentation { get; set; }
    }
}
