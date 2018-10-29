using Drive.Client.Helpers.Localize;
using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Resources.Resx;
using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace Drive.Client.ViewModels {
    public class PolandDriveAutoDetailsViewModel : ContentPageBaseViewModel {

        public PolandDriveAutoDetailsViewModel() {
            ActionBarViewModel = DependencyLocator.Resolve<CommonActionBarViewModel>();
            ActionBarViewModel.InitializeAsync(this);


            VehicleTechnicalInspectionFormattedString.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            VehicleTechnicalInspectionFormattedString.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            VehicleTechnicalInspectionFormattedString.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.VehicleTechnicalInspection))));

            CivilLiabilityInsuranceFormattedString.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            CivilLiabilityInsuranceFormattedString.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            CivilLiabilityInsuranceFormattedString.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.CivilLiabilityInsurance))));
        }

        private FormattedString _vehicleTechnicalInspectionFormattedString = new FormattedString();
        public FormattedString VehicleTechnicalInspectionFormattedString {
            get => _vehicleTechnicalInspectionFormattedString;
            private set => SetProperty<FormattedString>(ref _vehicleTechnicalInspectionFormattedString, value);
        }

        private FormattedString _civilLiabilityInsuranceFormattedString = new FormattedString();
        public FormattedString CivilLiabilityInsuranceFormattedString {
            get => _civilLiabilityInsuranceFormattedString;
            private set => SetProperty<FormattedString>(ref _civilLiabilityInsuranceFormattedString, value);
        }

        PolandVehicleDetail _polandDriveAuto;
        public PolandVehicleDetail PolandDriveAuto {
            get => _polandDriveAuto;
            private set => SetProperty(ref _polandDriveAuto, value);
        }

        public override void Dispose() {
            base.Dispose();

            ActionBarViewModel?.Dispose();
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is PolandVehicleDetail polandVehicleDetail) {
                PolandDriveAuto = polandVehicleDetail;

                VehicleTechnicalInspectionFormattedString.Spans.Last().Text = string.Format(": {0}", polandVehicleDetail.VehicleTechnicalInspection);
                CivilLiabilityInsuranceFormattedString.Spans.Last().Text = string.Format(": {0}", polandVehicleDetail.CivilLiabilityInsurance);
            }

            ActionBarViewModel?.InitializeAsync(navigationData);

            return base.InitializeAsync(navigationData);
        }
    }
}
