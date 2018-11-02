using Drive.Client.Models.Identities.Posts;
using Drive.Client.Services.OpenUrl;

namespace Drive.Client.ViewModels.BottomTabViewModels.Home.Post {
    public class MediaPostViewModel : PostBaseViewModel {

        string _mediaUrl;
        public string MediaUrl {
            get => _mediaUrl;
            private set => SetProperty<string>(ref _mediaUrl, value);
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        /// <param name="openUrlService"></param>
        public MediaPostViewModel() {
           
        }

        protected override void OnPost(PostBase post) {
            base.OnPost(post);

            if (post is MediaPost mediaPost) {
                MediaUrl = mediaPost.MediaUrl;
            } else {
                MediaUrl = null;
            }
        }
    }
}
