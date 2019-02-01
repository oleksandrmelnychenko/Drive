using Drive.Client.Factories.ObjectToSelectorDataItem;
using Drive.Client.Helpers;
using Drive.Client.Models.Calculator;
using Drive.Client.Models.Calculator.TODO;
using Drive.Client.Models.DataItems;
using Drive.Client.Resources.Resx;
using Drive.Client.Services.Customs;
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

        /// <summary>
        /// TODO: temporary hardcoded data
        /// </summary>
        private List<CommonDataItem<VehicleType>> _vehicleTypes = null;
        /// <summary>
        /// TODO: temporary hardcoded data
        /// </summary>
        private List<CommonDataItem<int>> _vehicleAges = null;
        /// <summary>
        /// TODO: temporary hardcoded data
        /// </summary>
        private List<CommonDataItem<double>> _engineCapacities = null;


        private SelectorPopupViewModel _selectorPopupViewModel;
        public SelectorPopupViewModel SelectorPopupViewModel {
            get => _selectorPopupViewModel;
            private set => SetProperty<SelectorPopupViewModel>(ref _selectorPopupViewModel, value);
        }

        private CommonDataItem<VehicleType> _selectedVehicleType;
        public CommonDataItem<VehicleType> SelectedVehicleType {
            get => _selectedVehicleType;
            set => SetProperty<CommonDataItem<VehicleType>>(ref _selectedVehicleType, value);
        }

        CommonDataItem<int> _selectedVehicleAge;
        public CommonDataItem<int> SelectedVehicleAge {
            get => _selectedVehicleAge;
            set => SetProperty<CommonDataItem<int>>(ref _selectedVehicleAge, value);
        }

        CommonDataItem<double> _selectedEngineCapacity;
        public CommonDataItem<double> SelectedEngineCapacity {
            get => _selectedEngineCapacity;
            set => SetProperty<CommonDataItem<double>>(ref _selectedEngineCapacity, value);
        }

        List<CommonDataItem<Currency>> _currencies;
        public List<CommonDataItem<Currency>> Currencies {
            get => _currencies;
            private set => SetProperty<List<CommonDataItem<Currency>>>(ref _currencies, value);
        }

        CommonDataItem<Currency> _selectedCurrency;
        public CommonDataItem<Currency> SelectedCurrency {
            get => _selectedCurrency;
            set => SetProperty<CommonDataItem<Currency>>(ref _selectedCurrency, value);
        }

        List<CommonDataItem<EngineType>> _engines;
        public List<CommonDataItem<EngineType>> Engines {
            get => _engines;
            private set => SetProperty<List<CommonDataItem<EngineType>>>(ref _engines, value);
        }

        CommonDataItem<EngineType> _selectedEngine;
        public CommonDataItem<EngineType> SelectedEngine {
            get => _selectedEngine;
            set => SetProperty<CommonDataItem<EngineType>>(ref _selectedEngine, value);
        }

        double _vehicleCost;
        public double VehicleCost {
            get => _vehicleCost;
            set => SetProperty<double>(ref _vehicleCost, value);
        }

        double _vehicleFullMass;
        public double VehicleFullMass {
            get => _vehicleFullMass;
            set => SetProperty<double>(ref _vehicleFullMass, value);
        }

        bool _isGracePeriodTakenIntoAccount;
        public bool IsGracePeriodTakenIntoAccount {
            get => _isGracePeriodTakenIntoAccount;
            set => SetProperty<bool>(ref _isGracePeriodTakenIntoAccount, value);
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

        /// <summary>
        ///     ctor().
        /// </summary>
        /// <param name="objectToSelectorDataItemFactory"></param>
        public CalculatorViewModel(IObjectToSelectorDataItemFactory objectToSelectorDataItemFactory, ICustomsService customsService) {
            _objectToSelectorDataItemFactory = objectToSelectorDataItemFactory;
            _customsService = customsService;

            SelectorPopupViewModel = DependencyLocator.Resolve<SelectorPopupViewModel>();
            SelectorPopupViewModel.InitializeAsync(this);
            SelectorPopupViewModel.ItemSelected += OnSelectorPopupViewModelItemSelected;

            GetData();
        }

        public override Task InitializeAsync(object navigationData) {

            SelectorPopupViewModel?.InitializeAsync(navigationData);

            return base.InitializeAsync(navigationData);
        }

        public override void Dispose() {
            base.Dispose();

            SelectorPopupViewModel.ItemSelected -= OnSelectorPopupViewModelItemSelected;
            SelectorPopupViewModel?.Dispose();
        }

        private void OnCalculate() {
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
           
            var tt = _customsService.CalculateCustoms(new CarCustoms {
                EngineCap = 3000,
                EngineType = "Gasoline",
                PreferentialExcise = false,
                Price = 5000,
                Year = 10
            });
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
    }
}
