using Drive.Client.Views.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.Posts {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostCommentsView : ContentPageBaseView {

        /// <summary>
        ///     ctor().
        /// </summary>
        public PostCommentsView() {
            InitializeComponent();
        }

        private void ListViewItemSelected(object sender, SelectedItemChangedEventArgs e) {
            _comments_listView.SelectedItem = null;
        }
    }
}