using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.IdentityAccounting.EditProfile {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPhoneNumberView : StepBaseView {
        public EditPhoneNumberView() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            _entyEx.Focus();
        }
    }
}