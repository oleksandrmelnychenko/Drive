using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.BottomTabViews.Home {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentView {
        public HomeView() {
            InitializeComponent();
        }
       
        private void ListViewItemSelected(object sender, SelectedItemChangedEventArgs e) {
            _posts_listView.SelectedItem = null;
        }
    }
}