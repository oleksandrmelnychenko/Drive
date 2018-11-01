using Drive.Client.Models.Identities.Posts;
using Drive.Client.Services.OpenUrl;
using Drive.Client.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.BottomTabViewModels.Home.Post {
    public abstract class SinglePostBaseViewModel : NestedViewModel {

        protected readonly IOpenUrlService OpenUrlService;

        /// <summary>
        ///     ctor().
        /// </summary>
        public SinglePostBaseViewModel() {
            OpenUrlService = DependencyLocator.Resolve<IOpenUrlService>();
        }

        /// <summary>
        /// Post instance.
        /// </summary>
        PostBase _post;
        public PostBase Post {
            get => _post;
            set {
                SetProperty(ref _post, value);
                OnPost(value);
            }
        }

        string _authorAvatarUrl;
        public string AuthorAvatarUrl {
            get => _authorAvatarUrl;
            private set => SetProperty<string>(ref _authorAvatarUrl, value);
        }

        string _authorName;
        public string AuthorName {
            get => _authorName;
            private set => SetProperty<string>(ref _authorName, value);
        }

        DateTime _publishDate;
        public DateTime PublishDate {
            get => _publishDate;
            private set => SetProperty<DateTime>(ref _publishDate, value);
        }

        string _mainMessage;
        public string MainMessage {
            get => _mainMessage;
            private set => SetProperty<string>(ref _mainMessage, value);
        }

        protected virtual void OnPost(PostBase post) {
            if (post != null) {
                AuthorAvatarUrl = post.AuthorAvatar;
                AuthorName = post.AuthorName;
                PublishDate = post.PublishDate;
                MainMessage = post.PostMessage;
            }
            else {
                AuthorAvatarUrl = null;
                AuthorName = null;
                PublishDate = default(DateTime);
                MainMessage = null;
            }
        }

        public override Task InitializeAsync(object navigationData) {


            return base.InitializeAsync(navigationData);
        }
    }
}
