using Drive.Client.Services.OpenUrl;

namespace Drive.Client.ViewModels.BottomTabViewModels.HomePosts.Post {
    public class TextPostViewModel : SinglePostBaseViewModel {

        public TextPostViewModel(IOpenUrlService openUrlService)
            : base(openUrlService) { }
    }
}
