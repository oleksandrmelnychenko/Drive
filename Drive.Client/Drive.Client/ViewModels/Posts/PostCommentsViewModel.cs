using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.BottomTabViewModels.Home.Post;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.Posts {
    internal sealed class PostCommentsViewModel : ContentPageBaseViewModel {

        PostBaseViewModel _currentPost;
        public PostBaseViewModel CurrentPost {
            get { return _currentPost; }
            set { SetProperty(ref _currentPost, value); }
        }

        ObservableCollection<CommentViewModel> _comments;
        public ObservableCollection<CommentViewModel> Comments {
            get { return _comments; }
            set { SetProperty(ref _comments, value); }
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

                Comments = GetComments(postBaseViewModel.Post.AnnounceBody.CommentsCount);
            }

            return base.InitializeAsync(navigationData);
        }

        private ObservableCollection<CommentViewModel> GetComments(int commentsCount) {
            ObservableCollection<CommentViewModel> commentViewModels = new ObservableCollection<CommentViewModel>();

            for (int i = 0; i < commentsCount; i++) {
                commentViewModels.Add(new CommentViewModel {
                    AuthorName = $"AuthorName{i}",
                    Comment = "The following example creates a dictionary collection of objects of type Box with an equality comparer. Two boxes are considered equal if their dimensions are the same. It then adds the boxes to the collection.",
                    PublishDate = DateTime.Now
                });
            }

            return commentViewModels;
        }
    }
}
