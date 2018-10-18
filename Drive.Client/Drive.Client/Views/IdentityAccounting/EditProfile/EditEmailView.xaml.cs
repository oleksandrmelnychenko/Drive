using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.IdentityAccounting.EditProfile {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditEmailView : StepBaseView {
        public EditEmailView() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            _entyEx.Focus();
        }
    }
}