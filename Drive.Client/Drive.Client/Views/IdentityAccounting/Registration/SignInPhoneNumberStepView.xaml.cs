using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.IdentityAccounting.Registration {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPhoneNumberStepView : IdentityAccountingStepBaseView {
        public SignInPhoneNumberStepView() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            _entyEx?.Focus();
        }
    }
}