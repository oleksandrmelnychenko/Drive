using Drive.Client.Factories.ObjectToSelectorDataItem;
using Drive.Client.Helpers;
using Drive.Client.Models.DataItems;
using Drive.Client.Models.EntityModels.TODO;
using Drive.Client.Resources.Resx;
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

        /// <summary>
        /// TODO: temporary hardcoded data
        /// </summary>
        private List<CommonDataItem<VehicleType>> _vehicleTypes = null;
        /// <summary>
        /// TODO: temporary hardcoded data
        /// </summary>
        private List<CommonDataItem<string>> _vehicleAges = null;
        /// <summary>
        /// TODO: temporary hardcoded data
        /// </summary>
        private List<CommonDataItem<double>> _engineCapacities = null;

        public CalculatorViewModel(
            IObjectToSelectorDataItemFactory objectToSelectorDataItemFactory) {

            _objectToSelectorDataItemFactory = objectToSelectorDataItemFactory;

            SelectorPopupViewModel = DependencyLocator.Resolve<SelectorPopupViewModel>();
            SelectorPopupViewModel.InitializeAsync(this);
            SelectorPopupViewModel.ItemSelected += OnSelectorPopupViewModelItemSelected;

            TODO_HARDCODED_EXTRACTION();
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

        public ICommand CalculateCommand => new Command(async () => {
            await DialogService.ToastAsync("CalculateCommand in developing");
        });

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

        private CommonDataItem<string> _selectedVehicleAge;
        public CommonDataItem<string> SelectedVehicleAge {
            get => _selectedVehicleAge;
            set => SetProperty<CommonDataItem<string>>(ref _selectedVehicleAge, value);
        }

        private CommonDataItem<double> _selectedEngineCapacity;
        public CommonDataItem<double> SelectedEngineCapacity {
            get => _selectedEngineCapacity;
            set => SetProperty<CommonDataItem<double>>(ref _selectedEngineCapacity, value);
        }

        private List<CommonDataItem<Currency>> _currencies;
        public List<CommonDataItem<Currency>> Currencies {
            get => _currencies;
            private set => SetProperty<List<CommonDataItem<Currency>>>(ref _currencies, value);
        }

        private CommonDataItem<Currency> _selectedCurrency;
        public CommonDataItem<Currency> SelectedCurrency {
            get => _selectedCurrency;
            set => SetProperty<CommonDataItem<Currency>>(ref _selectedCurrency, value);
        }

        private List<CommonDataItem<EngineType>> _engines;
        public List<CommonDataItem<EngineType>> Engines {
            get => _engines;
            private set => SetProperty<List<CommonDataItem<EngineType>>>(ref _engines, value);
        }

        private CommonDataItem<EngineType> _selectedEngine;
        public CommonDataItem<EngineType> SelectedEngine {
            get => _selectedEngine;
            set => SetProperty<CommonDataItem<EngineType>>(ref _selectedEngine, value);
        }

        private double _vehicleCost;
        public double VehicleCost {
            get => _vehicleCost;
            set => SetProperty<double>(ref _vehicleCost, value);
        }

        private bool _isGracePeriodTakenIntoAccount;
        public bool IsGracePeriodTakenIntoAccount {
            get => _isGracePeriodTakenIntoAccount;
            set => SetProperty<bool>(ref _isGracePeriodTakenIntoAccount, value);
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

        protected override void TabbViewModelInit() {
            TabIcon = IconPath.CALCULATOR;
            RelativeViewType = typeof(CalculatorView);
            HasBackgroundItem = false;
        }

        private async void OnSelectorPopupViewModelItemSelected(object sender, IPopupSelectionItem e) {
            if (e is CommonDataItem<VehicleType> vehicleType) {
                SelectedVehicleType = vehicleType;
            }
            else if (e is CommonDataItem<string> vehicleAge) {
                SelectedVehicleAge = vehicleAge;
            }
            else if (e is CommonDataItem<double> engineCapacity) {
                SelectedEngineCapacity = engineCapacity;
            }
            else {
                Debugger.Break();
                await DialogService.ToastAsync("Unsuported selection type");
            }
        }

        private void TODO_HARDCODED_EXTRACTION() {
            Currencies = _objectToSelectorDataItemFactory.BuildCommonDataItems(Enum.GetValues(typeof(Currency)).Cast<Currency>().ToArray<Currency>());
            SelectedCurrency = Currencies.First();

            Engines = _objectToSelectorDataItemFactory.BuildCommonDataItems(Enum.GetValues(typeof(EngineType)).Cast<EngineType>().ToArray<EngineType>());
            SelectedEngine = Engines.First();

            _vehicleTypes = _objectToSelectorDataItemFactory.BuildCommonDataItems(Enum.GetValues(typeof(VehicleType)).Cast<VehicleType>().ToArray<VehicleType>());
            SelectedVehicleType = _vehicleTypes.First();

            _vehicleAges = _objectToSelectorDataItemFactory.BuildCommonDataItems(new string[] { "1", "2", "3", "4", "5", "6", "7", "15 and more" });
            SelectedVehicleAge = _vehicleAges.First();

            _engineCapacities = _objectToSelectorDataItemFactory.BuildCommonDataItems(new double[] { 0.5, 1, 1.5, 2.5, 3.5, 7.7, 8.9 });
            SelectedEngineCapacity = _engineCapacities.First();
        }
    }
}
