using Drive.Client.Views.IdentityAccounting;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.Search {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchByPolandCarIdFinallyStepView : StepBaseView {
        public SearchByPolandCarIdFinallyStepView() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            _entyEx?.Focus();
        }
    }
}