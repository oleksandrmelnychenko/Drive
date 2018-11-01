using Drive.Client.ViewModels.BottomTabViewModels.HomePosts.Post;
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
            if (item is MediaPostViewModel) {
                return _mediaPost;
            }
            else if (item is TextPostViewModel) {
                return _textPost;
            }
            else {
                Debugger.Break();
                throw new InvalidOperationException("HomePostTemplateSelector.OnSelectTemplate unsuported post model type.");
            }
        }
    }
}
