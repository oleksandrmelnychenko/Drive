using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.Resources.Resx;
using Drive.Client.Services.Vehicle;
using Drive.Client.Validations;
using Drive.Client.Validations.ValidationRules;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.IdentityAccounting;
using Drive.Client.ViewModels.Popups;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.Search {
    public sealed class SearchByPolandCarIdFinallyStepViewModel : StepBaseViewModel {

        private readonly IVehicleService _vehicleService;

        private SearchByPolandNumberArgs _searchByPolandNumberArgs;
        private CancellationTokenSource _getPolandVehicleCancellationTokenSource = new CancellationTokenSource();

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

            StepTitle = FIRST_DATE_REGISTRATION_TITLE;
            MainInputIconPath = DATE_ICON_PATH;
            MainInputPlaceholder = DATE_TITLE;

            RequestInfoPopupViewModel = DependencyLocator.Resolve<PolandRequestInfoPopupViewModel>();
            RequestInfoPopupViewModel.InitializeAsync(this);
        }

        public override void Dispose() {
            base.Dispose();

            RequestInfoPopupViewModel?.Dispose();
            ResetCancellationTokenSource(ref _getPolandVehicleCancellationTokenSource);
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

                    Guid busyKey = Guid.NewGuid();
                    SetBusy(busyKey, true);

                    ResetCancellationTokenSource(ref _getPolandVehicleCancellationTokenSource);
                    CancellationTokenSource cancellationTokenSource = _getPolandVehicleCancellationTokenSource;

                    PolandVehicleDetail foundPolandVehicle = null;

                    try {
                        _searchByPolandNumberArgs.Date = MainInput.Value.Replace('/', '.');

                        foundPolandVehicle = await _vehicleService.GetPolandVehicleDetails(_searchByPolandNumberArgs, cancellationTokenSource.Token);

                        RequestInfoPopupViewModel.ShowPopupCommand.Execute(foundPolandVehicle);
                    }
                    catch (OperationCanceledException) { }
                    catch (ObjectDisposedException) { }
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

            MainInput.Validations.Add(new StringToDateTimeRule<string>() { ValidationMessage = ResourceLoader.GetString(nameof(AppStrings.IncorrectDateFormat)) });
        }
    }
}
