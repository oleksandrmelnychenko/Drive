using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.BottomTabViewModels.Home.Post;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.Posts {
    internal sealed class PostCommentsViewModel : ContentPageBaseViewModel {

        PostBaseViewModel _currentPost;
        public PostBaseViewModel CurrentPost {
            get { return _currentPost; }
            set { SetProperty(ref _currentPost, value); }
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public PostCommentsViewModel() {
            ActionBarViewModel = DependencyLocator.Resolve<CommonActionBarViewModel>();
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is PostBaseViewModel postBaseViewModel) {
                CurrentPost = postBaseViewModel;
            }

            return base.InitializeAsync(navigationData);
        }
    }
}
