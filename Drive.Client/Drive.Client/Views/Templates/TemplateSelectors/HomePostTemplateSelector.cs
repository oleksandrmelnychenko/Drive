using Drive.Client.Models.Identities.Posts;
using Drive.Client.ViewModels.BottomTabViewModels.Home.Post;
using Drive.Client.Views.Templates.TemplateSelectors.ViewCells.Post;
using System;
using System.Diagnostics;
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
            var viewModel = item as PostBaseViewModel;

            if (viewModel == null) return null;

            return viewModel.Post.PostType == PostType.MediaPost ? _mediaPost : _textPost;
        }
    }
}
