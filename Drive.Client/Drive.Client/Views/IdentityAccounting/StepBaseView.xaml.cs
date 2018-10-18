using Drive.Client.Views.Base;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.IdentityAccounting {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StepBaseView : ContentPageBaseView {
        public StepBaseView() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            _entyEx?.Focus();
        }
    }
}