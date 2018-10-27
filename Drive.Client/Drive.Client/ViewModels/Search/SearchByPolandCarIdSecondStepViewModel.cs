using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.ViewModels.IdentityAccounting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.Search {
    public sealed class SearchByPolandCarIdSecondStepViewModel : StepBaseViewModel {

        private SearchByPolandNumberArgs _searchByPolandNumberArgs;

        /// <summary>
        ///     ctor().
        /// </summary>
        public SearchByPolandCarIdSecondStepViewModel() {
            StepTitle = VIN_STEP_TITLE;
            //MainInputPlaceholder = NAME_PLACEHOLDER_STEP_REGISTRATION;
            //MainInputIconPath = NAME_ICON_PATH;
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
