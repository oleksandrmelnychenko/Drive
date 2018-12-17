using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.ViewModels.BottomTabViewModels.Home.Post;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Drive.Client.Factories.Announcements {
    public interface IAnnouncementsFactory {
        ObservableCollection<PostBaseViewModel> BuildPostViewModels(IEnumerable<Announce> announces);

        PostBaseViewModel CreatePostViewModel(Announce announce);
    }
}
