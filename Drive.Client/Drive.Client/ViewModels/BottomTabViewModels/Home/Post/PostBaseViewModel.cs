using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.Services.OpenUrl;
using Drive.Client.ViewModels.Base;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels.Home.Post {
    public class PostBaseViewModel : NestedViewModel {

        private readonly IOpenUrlService OpenUrlService;

        string _authorAvatarUrl;
        public string AuthorAvatarUrl {
            get => _authorAvatarUrl;
            private set => SetProperty(ref _authorAvatarUrl, value);
        }

        string _authorName;
        public string AuthorName {
            get => _authorName;
            private set => SetProperty(ref _authorName, value);
        }

        DateTime _publishDate;
        public DateTime PublishDate {
            get => _publishDate;
            private set => SetProperty(ref _publishDate, value);
        }

        string _mainMessage;
        public string MainMessage {
            get => _mainMessage;
            private set => SetProperty(ref _mainMessage, value);
        }

        /// <summary>
        /// Post instance.
        /// </summary>
        Announce _post;
        public Announce Post {
            get => _post;
            set {
                SetProperty(ref _post, value);
                OnPost(value);
            }
        }

        public ICommand TestCommand => new Command(() => OnTest());

        /// <summary>
        ///     ctor().
        /// </summary>
        public PostBaseViewModel() {
            OpenUrlService = DependencyLocator.Resolve<IOpenUrlService>();
        }

        private void OnTest() {
        }

        protected virtual void OnPost(Announce post) {
            if (post != null) {
                AuthorAvatarUrl = post.AuthorAvatar;
                AuthorName = post.AuthorName;
                PublishDate = post.PublishDate;
                MainMessage = post.Content;
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
