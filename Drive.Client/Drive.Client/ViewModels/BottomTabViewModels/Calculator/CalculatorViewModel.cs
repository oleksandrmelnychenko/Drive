using Drive.Client.Factories.ObjectToSelectorDataItem;
using Drive.Client.Factories.Validation;
using Drive.Client.Helpers;
using Drive.Client.Helpers.Localize;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.Models.Calculator;
using Drive.Client.Models.Calculator.TODO;
using Drive.Client.Models.DataItems;
using Drive.Client.Resources.Resx;
using Drive.Client.Services.Customs;
using Drive.Client.Validations;
using Drive.Client.Validations.ValidationRules;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.Popups;
using Drive.Client.Views.BottomTabViews.Calculator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels.Calculator {
    public sealed class CalculatorViewModel : TabbedViewModelBase {

        private readonly IObjectToSelectorDataItemFactory _objectToSelectorDataItemFactory;

        private readonly ICustomsService _customsService;

        private readonly IValidationObjectFactory _validationObjectFactory;

        private List<CommonDataItem<VehicleType>> _vehicleTypes = null;
       
        private List<CommonDataItem<int>> _vehicleAges = null;
    
        private List<CommonDataItem<double>> _engineCapacities = null;

        private SelectorPopupViewModel _selectorPopupViewModel;
        public SelectorPopupViewModel SelectorPopupViewModel {
            get => _selectorPopupViewModel;
            private set => SetProperty(ref _selectorPopupViewModel, value);
        }

        CustomsResultPopupViewModel _customsResultPopupViewModel;
        public CustomsResultPopupViewModel CustomsResultPopupViewModel {
            get => _customsResultPopupViewModel;
            private set => SetProperty(ref _customsResultPopupViewModel, value);
        }

        private CommonDataItem<VehicleType> _selectedVehicleType;
        public CommonDataItem<VehicleType> SelectedVehicleType {
            get => _selectedVehicleType;
            set => SetProperty(ref _selectedVehicleType, value);
        }

        CommonDataItem<int> _selectedVehicleAge;
        public CommonDataItem<int> SelectedVehicleAge {
            get => _selectedVehicleAge;
            set => SetProperty(ref _selectedVehicleAge, value);
        }

        CommonDataItem<double> _selectedEngineCapacity;
        public CommonDataItem<double> SelectedEngineCapacity {
            get => _selectedEngineCapacity;
            set => SetProperty(ref _selectedEngineCapacity, value);
        }

        List<CommonDataItem<Currency>> _currencies;
        public List<CommonDataItem<Currency>> Currencies {
            get => _currencies;
            private set => SetProperty(ref _currencies, value);
        }

        CommonDataItem<Currency> _selectedCurrency;
        public CommonDataItem<Currency> SelectedCurrency {
            get => _selectedCurrency;
            set => SetProperty(ref _selectedCurrency, value);
        }

        List<CommonDataItem<EngineType>> _engines;
        public List<CommonDataItem<EngineType>> Engines {
            get => _engines;
            private set => SetProperty(ref _engines, value);
        }

        CommonDataItem<EngineType> _selectedEngine;
        public CommonDataItem<EngineType> SelectedEngine {
            get => _selectedEngine;
            set => SetProperty(ref _selectedEngine, value);
        }

        ValidatableObject<decimal> _vehicleCost;
        public ValidatableObject<decimal> VehicleCost {
            get => _vehicleCost;
            set => SetProperty(ref _vehicleCost, value);
        }

        double _vehicleFullMass;
        public double VehicleFullMass {
            get => _vehicleFullMass;
            set => SetProperty(ref _vehicleFullMass, value);
        }

        bool _isGracePeriodTakenIntoAccount;
        public bool IsGracePeriodTakenIntoAccount {
            get => _isGracePeriodTakenIntoAccount;
            set => SetProperty(ref _isGracePeriodTakenIntoAccount, value);
        }

        public ICommand SelectVehicleTypeCommand => new Command(() => {
            SelectorPopupViewModel.Title = ResourceLoader[nameof(AppStrings.VehicleTypeUppercase)];
            SelectorPopupViewModel.ShowPopupCommand.Execute(_vehicleTypes);
        });

        public ICommand SelectVehicleAgeCommand => new Command(() => {
            SelectorPopupViewModel.Title = ResourceLoader[nameof(AppStrings.VehicleAgeUppercase)];
            SelectorPopupViewModel.ShowPopupCommand.Execute(_vehicleAges);
        });

        public ICommand SelectEngineCapacityCommand => new Command(() => {
            SelectorPopupViewModel.Title = ResourceLoader[nameof(AppStrings.EngineCapacityUpperСase)];
            SelectorPopupViewModel.ShowPopupCommand.Execute(_engineCapacities);
        });

        public ICommand TakeGracePeriodIntoAccountCommand => new Command(() => IsGracePeriodTakenIntoAccount = !IsGracePeriodTakenIntoAccount);

        public ICommand CalculateCommand => new Command(() => OnCalculate());

        public ICommand InputTextChangedCommand => new Command(() => OnInputTextChanged());

        private void OnInputTextChanged() {
            if (_vehicleCost.Value > 0) {
                _vehicleCost.Validate();
            }
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        /// <param name="objectToSelectorDataItemFactory"></param>
        public CalculatorViewModel(IObjectToSelectorDataItemFactory objectToSelectorDataItemFactory,
                                   ICustomsService customsService,
                                   IValidationObjectFactory validationObjectFactory) {
            _objectToSelectorDataItemFactory = objectToSelectorDataItemFactory;
            _customsService = customsService;
            _validationObjectFactory = validationObjectFactory;

            SelectorPopupViewModel = DependencyLocator.Resolve<SelectorPopupViewModel>();
            SelectorPopupViewModel.InitializeAsync(this);
            SelectorPopupViewModel.ItemSelected += OnSelectorPopupViewModelItemSelected;

            CustomsResultPopupViewModel = DependencyLocator.Resolve<CustomsResultPopupViewModel>();
            CustomsResultPopupViewModel.InitializeAsync(this);

            GetData();
        }
      
        public override Task InitializeAsync(object navigationData) {
            if (navigationData is SelectedBottomBarTabArgs) {
                ClearData();
            }

            SelectorPopupViewModel?.InitializeAsync(navigationData);
            CustomsResultPopupViewModel?.InitializeAsync(navigationData);

            return base.InitializeAsync(navigationData);
        }

        private void ClearData() {
            VehicleCost = _validationObjectFactory.GetValidatableObject<decimal>();
            VehicleCost.Validations.Add(new NoZeroPriceRule<decimal> { ValidationMessage = ResourceLoader.GetString(nameof(AppStrings.FieldRequired)) });
        }

        public override void Dispose() {
            base.Dispose();

            SelectorPopupViewModel.ItemSelected -= OnSelectorPopupViewModelItemSelected;
            SelectorPopupViewModel?.Dispose();
            CustomsResultPopupViewModel?.Dispose();
        }

        private async void OnCalculate() {
            if (Validate()) {
                CustomsClearanceCalculatorFormBase calculatorFormBase = null;

                switch (SelectedVehicleType.Data) {
                    case VehicleType.Car:
                        calculatorFormBase = new CarCalculatorForm();
                        break;
                    case VehicleType.Truck:
                        calculatorFormBase = new TruckCalculatorForm();
                        break;
                    default:
                        Debugger.Break();
                        throw new InvalidOperationException("Unresolved vehicle type");
                }

                CustomsResult customsResult = await _customsService.CalculateCustoms(new CarCustoms {
                    EngineCap = SelectedEngineCapacity.Data,
                    EngineType = SelectedEngine.Data.ToString(),
                    PreferentialExcise = IsGracePeriodTakenIntoAccount,
                    Price = VehicleCost.Value,
                    Year = SelectedVehicleAge.Data,
                    Currency = SelectedCurrency.Data
                });

                CustomsResultPopupViewModel.ShowPopupCommand.Execute(customsResult);
            }
        }

        private bool Validate() {
            bool isValidVehicleCost = _vehicleCost.Validate();

            return isValidVehicleCost;
        }

        protected override void TabbViewModelInit() {
            TabIcon = IconPath.CALCULATOR;
            RelativeViewType = typeof(CalculatorView);
            HasBackgroundItem = false;
        }

        private async void OnSelectorPopupViewModelItemSelected(object sender, IPopupSelectionItem e) {
            if (e is CommonDataItem<VehicleType> vehicleType) {
                SelectedVehicleType = vehicleType;
            } else if (e is CommonDataItem<int> vehicleAge) {
                SelectedVehicleAge = vehicleAge;
            } else if (e is CommonDataItem<double> engineCapacity) {
                SelectedEngineCapacity = engineCapacity;
            } else {
                Debugger.Break();
                await DialogService.ToastAsync("Unsuported selection type");
            }
        }

        private void GetData() {
            Currencies = _objectToSelectorDataItemFactory.BuildCommonDataItems(Enum.GetValues(typeof(Currency)).Cast<Currency>().ToArray<Currency>());
            SelectedCurrency = Currencies.First();

            Engines = _objectToSelectorDataItemFactory.BuildCommonDataItems(Enum.GetValues(typeof(EngineType)).Cast<EngineType>().ToArray<EngineType>());
            SelectedEngine = Engines.First();

            _vehicleTypes = _objectToSelectorDataItemFactory.BuildCommonDataItems(Enum.GetValues(typeof(VehicleType)).OfType<VehicleType>().ToArray());
            SelectedVehicleType = _vehicleTypes.First();

            _vehicleAges = _objectToSelectorDataItemFactory.BuildCommonDataItems(new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" });
            SelectedVehicleAge = _vehicleAges.First();

            List<double> engineCap = new List<double> { 0.5 };
            for (int i = 0; i < 195; i++) {
                engineCap.Add(Math.Round(engineCap.LastOrDefault() + 0.1, 1));
            }
            _engineCapacities = _objectToSelectorDataItemFactory.BuildCommonDataItems(engineCap);
            SelectedEngineCapacity = _engineCapacities.First();
        }

        private void AddValidations() {
            _vehicleCost.Validations.Add(new NoZeroPriceRule<decimal> { ValidationMessage = ResourceLoader.GetString(nameof(AppStrings.FieldRequired)) });
        }
    }
}
