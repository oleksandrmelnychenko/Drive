using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.Services.Vehicle;
using Drive.Client.ViewModels.BottomTabViewModels.Bookmark;
using Drive.Client.ViewModels.IdentityAccounting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.Search {
    public sealed class SearchByPersonFinallyStepViewModel : StepBaseViewModel {

        private CancellationTokenSource _vehicleDetailsCancellationTokenSource = new CancellationTokenSource();

        private readonly IVehicleService _vehicleService;

        private SearchByPersonArgs _searchByPersonArgs;

        /// <summary>
        ///     ctor().
        /// </summary>
        public SearchByPersonFinallyStepViewModel(IVehicleService vehicleService) {
            _vehicleService = vehicleService;

            StepTitle = MIDDLENAME_STEP_REGISTRATION_TITLE;
            MainInputPlaceholder = MIDDLENAME_PLACEHOLDER_STEP_REGISTRATION;
            MainInputIconPath = NAME_ICON_PATH;
        }

        public override void Dispose() {
            base.Dispose();


        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is SearchByPersonArgs searchByPersonArgs) {
                _searchByPersonArgs = searchByPersonArgs;
            }

            return base.InitializeAsync(navigationData);
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                ResetCancellationTokenSource(ref _vehicleDetailsCancellationTokenSource);
                CancellationTokenSource cancellationTokenSource = _vehicleDetailsCancellationTokenSource;

                Guid busyKey = Guid.NewGuid();
                SetBusy(busyKey, true);

                if (_searchByPersonArgs != null) {
                    try {
                        _searchByPersonArgs.MiddleName = MainInput.Value;
                        VehicleDetailsByResidentFullName vehicleDetailsByResidentFullName = await _vehicleService.GetVehicleDetailsByResidentFullNameAsync(_searchByPersonArgs, cancellationTokenSource.Token);

                        if (vehicleDetailsByResidentFullName != null) {
                            await NavigationService.NavigateToAsync<MainViewModel>(new BottomTabIndexArgs { TargetTab = typeof(BookmarkViewModel) });
                        }
                    }
                    catch (Exception ex) {
                        Debug.WriteLine($"ERROR:{ex.Message}");
                        Debugger.Break();
                    }
                } else {
                    Debugger.Break();
                    await NavigationService.GoBackAsync();
                }
                SetBusy(busyKey, false);
            }
        }
    }
}
