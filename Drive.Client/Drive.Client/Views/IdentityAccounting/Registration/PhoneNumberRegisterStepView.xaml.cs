using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.IdentityAccounting.Registration {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhoneNumberRegisterStepView : StepBaseView {
        public PhoneNumberRegisterStepView() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            _entyEx?.Focus();
        }
    }
}