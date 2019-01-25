using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.BottomTabViews.Profile {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserVehiclesView : ContentView {
        public UserVehiclesView() {
            InitializeComponent();
        }

        private void _list_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            _list.SelectedItem = null;
        }
    }
}