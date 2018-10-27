using Drive.Client.Models.Identities.Posts;
using Drive.Client.Services.OpenUrl;

namespace Drive.Client.ViewModels.BottomTabViewModels.HomePosts.Post {
    public class MediaPostViewModel : SinglePostBaseViewModel {

        public MediaPostViewModel(IOpenUrlService openUrlService)
            : base(openUrlService) { }

        string _mediaUrl;
        public string MediaUrl {
            get => _mediaUrl;
            private set => SetProperty<string>(ref _mediaUrl, value);
        }

        protected override void OnPost(PostBase post) {
            base.OnPost(post);

            if (post is MediaPost mediaPost) {
                MediaUrl = mediaPost.MediaUrl;
            }
            else {
                MediaUrl = null;
            }
        }
    }
}
