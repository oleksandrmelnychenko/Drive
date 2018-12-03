using Drive.Client.Helpers.Localize;
using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Resources.Resx;
using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.ViewModels.BottomTabViewModels.Bookmark;

namespace Drive.Client.ViewModels {
    public class PolandDriveAutoDetailsViewModel : ContentPageBaseViewModel {

        public PolandDriveAutoDetailsViewModel() {
            ActionBarViewModel = DependencyLocator.Resolve<CommonActionBarViewModel>();
            ActionBarViewModel.InitializeAsync(this);

            Year.Spans.Add(BuildHeaderSpan(nameof(AppStrings.YearOfCreation)));
            Year.Spans.Add(BuildValueSpan());

            Vin.Spans.Add(BuildHeaderSpan(nameof(AppStrings.VinUppercase)));
            Vin.Spans.Add(BuildValueSpan());

            VehicleTechnicalInspection.Spans.Add(BuildHeaderSpan(nameof(AppStrings.VehicleTechnicalInspection)));
            VehicleTechnicalInspection.Spans.Add(BuildValueSpan());

            LastOdometrData.Spans.Add(BuildHeaderSpan(nameof(AppStrings.LastRecordedOdometerReading)));
            LastOdometrData.Spans.Add(BuildValueSpan());

            RegistrationStatus.Spans.Add(BuildHeaderSpan(nameof(AppStrings.RegistrationStatus)));
            RegistrationStatus.Spans.Add(BuildValueSpan());

            EngineCapacity.Spans.Add(BuildHeaderSpan(nameof(AppStrings.EngineCapacity)));
            EngineCapacity.Spans.Add(BuildValueSpan());

            EnginePower.Spans.Add(BuildHeaderSpan(nameof(AppStrings.EnginePower)));
            EnginePower.Spans.Add(BuildValueSpan());

            FuelType.Spans.Add(BuildHeaderSpan(nameof(AppStrings.FuelType)));
            FuelType.Spans.Add(BuildValueSpan());

            TotalCapacity.Spans.Add(BuildHeaderSpan(nameof(AppStrings.TotalСapacity)));
            TotalCapacity.Spans.Add(BuildValueSpan());

            NumberOfSeats.Spans.Add(BuildHeaderSpan(nameof(AppStrings.NumberOfSeats)));
            NumberOfSeats.Spans.Add(BuildValueSpan());

            CurbWeight.Spans.Add(BuildHeaderSpan(nameof(AppStrings.CurbWeight)));
            CurbWeight.Spans.Add(BuildValueSpan());

            MaximumLadenMassOfBrakedTrailer.Spans.Add(BuildHeaderSpan(nameof(AppStrings.MaximumLadenMassOfBrakedTrailer)));
            MaximumLadenMassOfBrakedTrailer.Spans.Add(BuildValueSpan());

            MaximumLadenMassOfUnbrakedTrailer.Spans.Add(BuildHeaderSpan(nameof(AppStrings.MaximumLadenMassOfUnbrakedTrailer)));
            MaximumLadenMassOfUnbrakedTrailer.Spans.Add(BuildValueSpan());

            MaximumPermissibleTowableMass.Spans.Add(BuildHeaderSpan(nameof(AppStrings.MaximumPermissibleTowableMass)));
            MaximumPermissibleTowableMass.Spans.Add(BuildValueSpan());

            NumberOfAxles.Spans.Add(BuildHeaderSpan(nameof(AppStrings.NumberOfAxles)));
            NumberOfAxles.Spans.Add(BuildValueSpan());

            DateСurrentVehicleRegistrationCertificateIssued.Spans.Add(BuildHeaderSpan(nameof(AppStrings.DateСurrentVehicleRegistrationCertificateIssued)));
            DateСurrentVehicleRegistrationCertificateIssued.Spans.Add(BuildValueSpan());

            DateVehicleRecordDocumentIssued.Spans.Add(BuildHeaderSpan(nameof(AppStrings.DateVehicleRecordDocumentIssued)));
            DateVehicleRecordDocumentIssued.Spans.Add(BuildValueSpan());

            CivilLiabilityInsurance.Spans.Add(BuildHeaderSpan(nameof(AppStrings.CivilLiabilityInsurance)));
            CivilLiabilityInsurance.Spans.Add(BuildValueSpan());

            Type.Spans.Add(BuildHeaderSpan(nameof(AppStrings.Type)));
            Type.Spans.Add(BuildValueSpan());

            Number.Spans.Add(BuildHeaderSpan(nameof(AppStrings.Number)));
            Number.Spans.Add(BuildValueSpan());
        }

        private FormattedString _number = new FormattedString();
        public FormattedString Number {
            get => _number;
            private set => SetProperty<FormattedString>(ref _number, value);
        }

        private FormattedString _type = new FormattedString();
        public FormattedString Type {
            get => _type;
            private set => SetProperty<FormattedString>(ref _type, value);
        }

        private FormattedString _civilLiabilityInsurance = new FormattedString();
        public FormattedString CivilLiabilityInsurance {
            get => _civilLiabilityInsurance;
            private set => SetProperty<FormattedString>(ref _civilLiabilityInsurance, value);
        }

        private FormattedString _dateVehicleRecordDocumentIssued = new FormattedString();
        public FormattedString DateVehicleRecordDocumentIssued {
            get => _dateVehicleRecordDocumentIssued;
            private set => SetProperty<FormattedString>(ref _dateVehicleRecordDocumentIssued, value);
        }

        private FormattedString _dateСurrentVehicleRegistrationCertificateIssued = new FormattedString();
        public FormattedString DateСurrentVehicleRegistrationCertificateIssued {
            get => _dateСurrentVehicleRegistrationCertificateIssued;
            private set => SetProperty<FormattedString>(ref _dateСurrentVehicleRegistrationCertificateIssued, value);
        }

        private FormattedString _numberOfAxles = new FormattedString();
        public FormattedString NumberOfAxles {
            get => _numberOfAxles;
            private set => SetProperty<FormattedString>(ref _numberOfAxles, value);
        }

        private FormattedString _maximumPermissibleTowableMass = new FormattedString();
        public FormattedString MaximumPermissibleTowableMass {
            get => _maximumPermissibleTowableMass;
            private set => SetProperty<FormattedString>(ref _maximumPermissibleTowableMass, value);
        }

        private FormattedString _maximumLadenMassOfUnbrakedTrailer = new FormattedString();
        public FormattedString MaximumLadenMassOfUnbrakedTrailer {
            get => _maximumLadenMassOfUnbrakedTrailer;
            private set => SetProperty<FormattedString>(ref _maximumLadenMassOfUnbrakedTrailer, value);
        }

        private FormattedString _maximumLadenMassOfBrakedTrailer = new FormattedString();
        public FormattedString MaximumLadenMassOfBrakedTrailer {
            get => _maximumLadenMassOfBrakedTrailer;
            private set => SetProperty<FormattedString>(ref _maximumLadenMassOfBrakedTrailer, value);
        }

        private FormattedString _curbWeight = new FormattedString();
        public FormattedString CurbWeight {
            get => _curbWeight;
            private set => SetProperty<FormattedString>(ref _curbWeight, value);
        }

        private FormattedString _numberOfSeats = new FormattedString();
        public FormattedString NumberOfSeats {
            get => _numberOfSeats;
            private set => SetProperty<FormattedString>(ref _numberOfSeats, value);
        }

        private FormattedString _totalCapacity = new FormattedString();
        public FormattedString TotalCapacity {
            get => _totalCapacity;
            private set => SetProperty<FormattedString>(ref _totalCapacity, value);
        }

        private FormattedString _fuelType = new FormattedString();
        public FormattedString FuelType {
            get => _fuelType;
            private set => SetProperty<FormattedString>(ref _fuelType, value);
        }

        private FormattedString _enginePower = new FormattedString();
        public FormattedString EnginePower {
            get => _enginePower;
            private set => SetProperty<FormattedString>(ref _enginePower, value);
        }

        private FormattedString _engineCapacity = new FormattedString();
        public FormattedString EngineCapacity {
            get => _engineCapacity;
            private set => SetProperty<FormattedString>(ref _engineCapacity, value);
        }

        private FormattedString _registrationStatus = new FormattedString();
        public FormattedString RegistrationStatus {
            get => _registrationStatus;
            private set => SetProperty<FormattedString>(ref _registrationStatus, value);
        }

        private FormattedString _lastOdometrData = new FormattedString();
        public FormattedString LastOdometrData {
            get => _lastOdometrData;
            private set => SetProperty<FormattedString>(ref _lastOdometrData, value);
        }

        private FormattedString _vehicleTechnicalInspection = new FormattedString();
        public FormattedString VehicleTechnicalInspection {
            get => _vehicleTechnicalInspection;
            private set => SetProperty<FormattedString>(ref _vehicleTechnicalInspection, value);
        }

        private FormattedString _vin = new FormattedString();
        public FormattedString Vin {
            get => _vin;
            private set => SetProperty<FormattedString>(ref _vin, value);
        }

        private FormattedString _year = new FormattedString();
        public FormattedString Year {
            get => _year;
            private set => SetProperty<FormattedString>(ref _year, value);
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
                if (NavigationService.CurrentViewModelsNavigationStack.FirstOrDefault() is MainViewModel mainViewModel) {
                    mainViewModel.InitializeAsync(new BottomTabIndexArgs() { TargetTab = typeof(BookmarkViewModel) });
                }

                PolandDriveAuto = polandVehicleDetail;

                Year.Spans.Last().Text = string.Format("  {0:dd.MM.yyyy}", PolandDriveAuto.Year);
                Vin.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.VIN);
                VehicleTechnicalInspection.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.VehicleTechnicalInspection);
                LastOdometrData.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.LastOdometrData);
                RegistrationStatus.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.RegistrationStatus);
                EngineCapacity.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.EngineCapacity);
                EnginePower.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.EnginePower);
                FuelType.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.FuelType);
                TotalCapacity.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.TotalCapacity);
                NumberOfSeats.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.NumberOfSeats);
                CurbWeight.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.CurbWeight);
                MaximumLadenMassOfBrakedTrailer.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.MaximumLadenMassOfBrakedTrailer);
                MaximumLadenMassOfUnbrakedTrailer.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.MaximumLadenMassOfUnbrakedTrailer);
                MaximumPermissibleTowableMass.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.MaximumPermissibleTowableMass);
                NumberOfAxles.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.NumberOfAxles);
                DateСurrentVehicleRegistrationCertificateIssued.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.DateСurrentVehicleRegistrationCertificateIssued);
                DateVehicleRecordDocumentIssued.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.DateVehicleRecordDocumentIssued);
                CivilLiabilityInsurance.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.CivilLiabilityInsurance);
                Type.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.Type);
                Number.Spans.Last().Text = string.Format("  {0}", PolandDriveAuto.Number);
            }

            ActionBarViewModel?.InitializeAsync(navigationData);

            return base.InitializeAsync(navigationData);
        }

        private Span BuildHeaderSpan(string stringPath) {
            Span span = new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            };

            span.SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.GetString(stringPath)));

            return span;
        }

        private Span BuildValueSpan() =>
            new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            };
    }
}
