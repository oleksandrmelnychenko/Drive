using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.Services.Vehicle;
using Drive.Client.Validations;
using Drive.Client.Validations.ValidationRules;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.IdentityAccounting;
using Drive.Client.ViewModels.Popups;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.Search {
    public sealed class SearchByPolandCarIdFinallyStepViewModel : StepBaseViewModel {

        private SearchByPolandNumberArgs _searchByPolandNumberArgs;
        private readonly IVehicleService _vehicleService;

        public ICommand InputTextChangedCommand => new Command(() => { });

        PolandRequestInfoPopupViewModel _requestInfoPopupViewModel;
        public PolandRequestInfoPopupViewModel RequestInfoPopupViewModel {
            get => _requestInfoPopupViewModel;
            private set {
                _requestInfoPopupViewModel?.Dispose();
                SetProperty(ref _requestInfoPopupViewModel, value);
            }
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public SearchByPolandCarIdFinallyStepViewModel(IVehicleService vehicleService) {
            _vehicleService = vehicleService;

            StepTitle = DATE_STEP_TITLE;
            MainInputIconPath = DATE_ICON_PATH;
            KeyboardType = Keyboard.Numeric;

            RequestInfoPopupViewModel = DependencyLocator.Resolve<PolandRequestInfoPopupViewModel>();
            RequestInfoPopupViewModel.InitializeAsync(this);
        }

        public override void Dispose() {
            base.Dispose();

            RequestInfoPopupViewModel?.Dispose();
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is SearchByPolandNumberArgs searchByPolandNumberArgs) {
                _searchByPolandNumberArgs = searchByPolandNumberArgs;
            }

            RequestInfoPopupViewModel?.InitializeAsync(navigationData);

            return base.InitializeAsync(navigationData);
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                if (_searchByPolandNumberArgs != null) {
                    PolandVehicleDetail foundPolandVehicle = null;

                    Guid busyKey = Guid.NewGuid();
                    SetBusy(busyKey, true);

                    try {
                        _searchByPolandNumberArgs.Date = MainInput.Value.Replace('/', '.');

                        foundPolandVehicle = await _vehicleService.GetPolandVehicleDetails(_searchByPolandNumberArgs);

                        RequestInfoPopupViewModel.ShowPopupCommand.Execute(foundPolandVehicle);
                    }
                    catch (Exception ex) {
                        Debug.WriteLine($"ERROR:{ex.Message}");
                    }

                    SetBusy(busyKey, false);
                }
                else {
                    await NavigationService.GoBackAsync();
                }
            }
        }

        protected override void ResetValidationObjects() {
            base.ResetValidationObjects();

            MainInput.Validations.Add(new StringToDateTimeRule<string>() { ValidationMessage = ValidatableObject<string>.INVALID_DATE_FORMAT_VALIDATION_MESSAGE });
        }
    }
}
