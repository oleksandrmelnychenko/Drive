using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.IdentityAccounting.EditProfile {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPhoneNumberView : IdentityAccountingStepBaseView {
        public EditPhoneNumberView() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            _entyEx.Focus();
        }
    }
}