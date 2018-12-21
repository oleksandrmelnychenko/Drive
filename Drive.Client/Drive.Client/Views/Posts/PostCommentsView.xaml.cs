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

        private void _comentInput_Entry_Focused(object sender, FocusEventArgs e) {
            _hackScroll_ScrollView.TranslationX = 0;
            _hackScroll_ScrollView.InputTransparent = false;
        }

        private void EntryExtended_Unfocused(object sender, FocusEventArgs e) {
            _hackScroll_ScrollView.TranslationX = 9999999;
            _hackScroll_ScrollView.InputTransparent = true;
        }
    }
}