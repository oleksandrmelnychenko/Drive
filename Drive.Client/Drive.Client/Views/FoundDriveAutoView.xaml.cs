using Drive.Client.Views.Base;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoundDriveAutoView : ContentPageBaseView {
        public FoundDriveAutoView() {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e) {
            _list.SelectedItem = null;
        }
    }
}