using Drive.Client.Views.IdentityAccounting;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.Search {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchByPolandCarIdSecondStepView : StepBaseView {
        public SearchByPolandCarIdSecondStepView() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            _entyEx?.Focus();
        }
    }
}