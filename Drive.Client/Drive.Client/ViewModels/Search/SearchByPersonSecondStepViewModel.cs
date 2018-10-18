using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.ViewModels.IdentityAccounting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.Search {
    public sealed class SearchByPersonSecondStepViewModel : StepBaseViewModel {

        private SearchByPersonArgs _searchByPersonArgs;

        /// <summary>
        ///     ctor().
        /// </summary>
        public SearchByPersonSecondStepViewModel() {
            StepTitle = NAME_STEP_REGISTRATION_TITLE;
            MainInputPlaceholder = NAME_PLACEHOLDER_STEP_REGISTRATION;
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
                if (_searchByPersonArgs != null) {
                    try {
                        _searchByPersonArgs.FirstName = MainInput.Value;
                        await NavigationService.NavigateToAsync<SearchByPersonFinallyStepViewModel>(_searchByPersonArgs);
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
