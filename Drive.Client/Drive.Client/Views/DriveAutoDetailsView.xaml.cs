using Drive.Client.Views.Base;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DriveAutoDetailsView : ContentPageBaseView {
        public DriveAutoDetailsView() {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e) {
            _list.SelectedItem = null;
        }
    }
}