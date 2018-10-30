using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.ViewModels.IdentityAccounting;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.Search {
    public sealed class SearchByPolandCarIdSecondStepViewModel : StepBaseViewModel {

        private SearchByPolandNumberArgs _searchByPolandNumberArgs;

        /// <summary>
        ///     ctor().
        /// </summary>
        public SearchByPolandCarIdSecondStepViewModel() {
            StepTitle = VIN_STEP_TITLE;
            MainInputPlaceholder = VEHICLE_VIN_CODE_PLACEHOLDER_STEP;
            MainInputIconPath = VEHICLE_VIN_CODE_ICON_PATH;
        }

        public override void Dispose() {
            base.Dispose();


        }

        protected override void ResetValidationObjects() {
            base.ResetValidationObjects();
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is SearchByPolandNumberArgs searchByPolandNumberArgs) {
                _searchByPolandNumberArgs = searchByPolandNumberArgs;
            }

            MainInput.Value = string.Empty;
#if DEBUG
            MainInput.Value = "wvwzzz1jzwb026870";
#endif

            return base.InitializeAsync(navigationData);
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                if (_searchByPolandNumberArgs != null) {
                    try {
                        _searchByPolandNumberArgs.Vin = MainInput.Value;
                        await NavigationService.NavigateToAsync<SearchByPolandCarIdFinallyStepViewModel>(_searchByPolandNumberArgs);
                    }
                    catch (Exception ex) {
                        Debug.WriteLine($"ERROR:{ex.Message}");
                        Debugger.Break();
                    }
                } else {
                    Debugger.Break();
                    await NavigationService.GoBackAsync();
                }
            }
        }
    }
}
