using Drive.Client.Views.Base;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehicleDetailView : ContentPageBaseView {
        public VehicleDetailView() {
            InitializeComponent();
        }

        private void _list_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e) {
            _list.SelectedItem = null;
        }
    }
}