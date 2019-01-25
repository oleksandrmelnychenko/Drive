using Drive.Client.DataItems.Calculator;
using Drive.Client.Helpers;
using Drive.Client.Models.DataItems.Calculator;
using Drive.Client.Models.EntityModels.Vehicle.Significance;
using Drive.Client.Resources.Resx;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.Popups;
using Drive.Client.Views.BottomTabViews.Calculator;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels.Calculator {
    public sealed class CalculatorViewModel : TabbedViewModelBase {

        private readonly ICalculatorEntitiesDataItems _calculatorEntitiesDataItems;

        public CalculatorViewModel(
            ICalculatorEntitiesDataItems calculatorEntitiesDataItems) {

            _calculatorEntitiesDataItems = calculatorEntitiesDataItems;

            SelectorPopupViewModel = DependencyLocator.Resolve<SelectorPopupViewModel>();
            SelectorPopupViewModel.InitializeAsync(this);
            SelectorPopupViewModel.ItemSelected += OnSelectorPopupViewModelItemSelected;

            Currencies = _calculatorEntitiesDataItems.GetCurrencyDataItems();
        }

        public ICommand SelectVehicleTypeCommand => new Command(() => {
            List<IPopupSelectionItem> vehicleTypes = new List<IPopupSelectionItem>() { new VehicleType() { Type = "Cat" }, new VehicleType() { Type = "Dog" } };

            SelectorPopupViewModel.Title = ResourceLoader[nameof(AppStrings.VehicleTypeUppercase)];
            SelectorPopupViewModel.ShowPopupCommand.Execute(vehicleTypes);
        });

        public ICommand SelectVehicleAgeCommand => new Command(() => {
            List<IPopupSelectionItem> vehicleAges = new List<IPopupSelectionItem>() { new VehicleAge() { Age = 1 }, new VehicleAge() { Age = 2 }, new VehicleAge() { Age = 3 }, new VehicleAge() { Age = 4 } };

            SelectorPopupViewModel.Title = ResourceLoader[nameof(AppStrings.VehicleAgeUppercase)];
            SelectorPopupViewModel.ShowPopupCommand.Execute(vehicleAges);
        });

        private SelectorPopupViewModel _selectorPopupViewModel;
        public SelectorPopupViewModel SelectorPopupViewModel {
            get => _selectorPopupViewModel;
            private set => SetProperty<SelectorPopupViewModel>(ref _selectorPopupViewModel, value);
        }

        private List<CurrencyDataItem> _currencies;
        public List<CurrencyDataItem> Currencies {
            get => _currencies;
            private set => SetProperty<List<CurrencyDataItem>>(ref _currencies, value);
        }

        private CurrencyDataItem _selectedCurrency;
        public CurrencyDataItem SelectedCurrency {
            get => _selectedCurrency;
            private set => SetProperty<CurrencyDataItem>(ref _selectedCurrency, value);
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
            if (e is VehicleType vehicleType) {
                await DialogService.ToastAsync(string.Format("Vehicle type selected {0}", vehicleType.Type));
            }
            else if (e is VehicleAge vehicleAge) {
                await DialogService.ToastAsync(string.Format("Vehicle age selected {0}", vehicleAge.Age));
            }
            else {
                Debugger.Break();
                await DialogService.ToastAsync("Unsuported selection type");
            }
        }
    }
}
