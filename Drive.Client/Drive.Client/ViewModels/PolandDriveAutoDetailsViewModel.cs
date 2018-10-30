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

            //VehicleTechnicalInspectionFormattedString.Spans.Add(new Span() {
            //    TextColor = (Color)Application.Current.Resources["HardGrayColor"],
            //    FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
            //    FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            //});
            //VehicleTechnicalInspectionFormattedString.Spans.Add(new Span() {
            //    TextColor = (Color)Application.Current.Resources["BlackColor"],
            //    FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
            //    FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            //});
            //VehicleTechnicalInspectionFormattedString.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.VehicleTechnicalInspection))));

            //CivilLiabilityInsuranceFormattedString.Spans.Add(new Span() {
            //    TextColor = (Color)Application.Current.Resources["HardGrayColor"],
            //    FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
            //    FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            //});
            //CivilLiabilityInsuranceFormattedString.Spans.Add(new Span() {
            //    TextColor = (Color)Application.Current.Resources["BlackColor"],
            //    FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
            //    FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            //});
            //CivilLiabilityInsuranceFormattedString.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.CivilLiabilityInsurance))));

            Year.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            Year.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            Year.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.YearOfCreation))));

            Vin.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            Vin.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            Vin.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.VinUppercase))));

            VehicleTechnicalInspection.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            VehicleTechnicalInspection.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            VehicleTechnicalInspection.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.VehicleTechnicalInspection))));

            LastOdometrData.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            LastOdometrData.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            LastOdometrData.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.LastRecordedOdometerReading))));

            RegistrationStatus.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            RegistrationStatus.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            RegistrationStatus.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.RegistrationStatus))));

            EngineCapacity.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            EngineCapacity.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            EngineCapacity.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.EngineCapacity))));

            EnginePower.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            EnginePower.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            EnginePower.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.EnginePower))));

            FuelType.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            FuelType.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            FuelType.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.FuelType))));

            TotalCapacity.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            TotalCapacity.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            TotalCapacity.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.TotalСapacity))));

            NumberOfSeats.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            NumberOfSeats.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            NumberOfSeats.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.NumberOfSeats))));

            CurbWeight.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            CurbWeight.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            CurbWeight.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.CurbWeight))));

            MaximumLadenMassOfBrakedTrailer.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            MaximumLadenMassOfBrakedTrailer.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            MaximumLadenMassOfBrakedTrailer.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.MaximumLadenMassOfBrakedTrailer))));

            MaximumLadenMassOfUnbrakedTrailer.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            MaximumLadenMassOfUnbrakedTrailer.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            MaximumLadenMassOfUnbrakedTrailer.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.MaximumLadenMassOfUnbrakedTrailer))));

            MaximumPermissibleTowableMass.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            MaximumPermissibleTowableMass.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            MaximumPermissibleTowableMass.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.MaximumPermissibleTowableMass))));

            NumberOfAxles.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            NumberOfAxles.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            NumberOfAxles.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.NumberOfAxles))));

            DateСurrentVehicleRegistrationCertificateIssued.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            DateСurrentVehicleRegistrationCertificateIssued.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            DateСurrentVehicleRegistrationCertificateIssued.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.DateСurrentVehicleRegistrationCertificateIssued))));

            DateVehicleRecordDocumentIssued.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            DateVehicleRecordDocumentIssued.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            DateVehicleRecordDocumentIssued.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.DateVehicleRecordDocumentIssued))));

            CivilLiabilityInsurance.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            CivilLiabilityInsurance.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            CivilLiabilityInsurance.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.CivilLiabilityInsurance))));

            Type.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            Type.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            Type.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.Type))));

            Number.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["HardGrayColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            Number.Spans.Add(new Span() {
                TextColor = (Color)Application.Current.Resources["BlackColor"],
                FontSize = Device.RuntimePlatform == Device.iOS ? 14 : 12,
                FontFamily = Device.RuntimePlatform == Device.iOS ? "SFProDisplay-Light" : "SFProDisplay-Light.ttf#SF Pro Display light",
            });
            Number.Spans.First().SetBinding(Span.TextProperty, new Binding("Value", source: ResourceLoader.Instance.GetString(nameof(AppStrings.Number))));

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





        //private FormattedString _vehicleTechnicalInspectionFormattedString = new FormattedString();
        //public FormattedString VehicleTechnicalInspectionFormattedString {
        //    get => _vehicleTechnicalInspectionFormattedString;
        //    private set => SetProperty<FormattedString>(ref _vehicleTechnicalInspectionFormattedString, value);
        //}

        //private FormattedString _civilLiabilityInsuranceFormattedString = new FormattedString();
        //public FormattedString CivilLiabilityInsuranceFormattedString {
        //    get => _civilLiabilityInsuranceFormattedString;
        //    private set => SetProperty<FormattedString>(ref _civilLiabilityInsuranceFormattedString, value);
        //}



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

                Year.Spans.Last().Text = string.Format(" {0:dd.MM.yyyy}", PolandDriveAuto.Year);
                Vin.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.VIN);
                VehicleTechnicalInspection.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.VehicleTechnicalInspection);
                LastOdometrData.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.LastOdometrData);
                RegistrationStatus.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.RegistrationStatus);
                EngineCapacity.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.EngineCapacity);
                EnginePower.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.EnginePower);
                FuelType.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.FuelType);
                TotalCapacity.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.TotalCapacity);
                NumberOfSeats.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.NumberOfSeats);
                CurbWeight.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.CurbWeight);
                MaximumLadenMassOfBrakedTrailer.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.MaximumLadenMassOfBrakedTrailer);
                MaximumLadenMassOfUnbrakedTrailer.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.MaximumLadenMassOfUnbrakedTrailer);
                MaximumPermissibleTowableMass.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.MaximumPermissibleTowableMass);
                NumberOfAxles.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.NumberOfAxles);
                DateСurrentVehicleRegistrationCertificateIssued.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.DateСurrentVehicleRegistrationCertificateIssued);
                DateVehicleRecordDocumentIssued.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.DateVehicleRecordDocumentIssued);
                CivilLiabilityInsurance.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.CivilLiabilityInsurance);
                Type.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.Type);
                Number.Spans.Last().Text = string.Format(" {0}", PolandDriveAuto.Number);
            }

            ActionBarViewModel?.InitializeAsync(navigationData);

            return base.InitializeAsync(navigationData);
        }
    }
}
