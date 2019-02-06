using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.IdentityAccounting.Registration {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPasswordStepView : StepBaseView {
        public SignInPasswordStepView() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            _entyEx?.Focus();
        }
    }
}