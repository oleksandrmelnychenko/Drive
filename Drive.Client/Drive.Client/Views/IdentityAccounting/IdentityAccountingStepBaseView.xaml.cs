using Drive.Client.Views.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.IdentityAccounting {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IdentityAccountingStepBaseView : ContentPageBaseView {
        public IdentityAccountingStepBaseView() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            _entyEx?.Focus();
        }
    }
}