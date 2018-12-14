using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.ViewModels.BottomTabViewModels.Home.Post;
using Drive.Client.Views.Templates.TemplateSelectors.ViewCells.Post;
using Xamarin.Forms;

namespace Drive.Client.Views.Templates.TemplateSelectors {
    public class HomePostTemplateSelector : DataTemplateSelector {

        private readonly DataTemplate _textPost;
        private readonly DataTemplate _mediaPost;

        public HomePostTemplateSelector() {
            _textPost = new DataTemplate(typeof(TextPostViewCell));
            _mediaPost = new DataTemplate(typeof(MediaPostViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {
            if (!(item is PostBaseViewModel viewModel)) return null;

            return viewModel.Post.AnnounceBody.Type == AnnounceType.Text ? _textPost : _mediaPost;
        }
    }
}
