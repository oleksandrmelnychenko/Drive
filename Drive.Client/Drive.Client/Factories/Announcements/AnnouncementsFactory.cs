using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.ViewModels.BottomTabViewModels.Home.Post;
using Xamarin.Forms.Internals;

namespace Drive.Client.Factories.Announcements {
    internal class AnnouncementsFactory : IAnnouncementsFactory {
        public ObservableCollection<PostBaseViewModel> BuildPostViewModels(IEnumerable<Announce> announces) {
            ObservableCollection<PostBaseViewModel> postBaseViewModels = new ObservableCollection<PostBaseViewModel>();

            List<Announce> listedAnnounces = announces.ToList();

            var sortedByDate = from a in listedAnnounces
                               orderby a.AnnounceBody.Created descending
                               select a;

            foreach (var announce in sortedByDate) {
                PostBaseViewModel postBaseViewModel = new PostBaseViewModel {
                    Post = announce
                };
                postBaseViewModels.Add(postBaseViewModel);
            }

            return postBaseViewModels;
        }

        public PostBaseViewModel CreatePostViewModel(Announce announce) {
            return new PostBaseViewModel { Post = announce };
        }
    }
}
