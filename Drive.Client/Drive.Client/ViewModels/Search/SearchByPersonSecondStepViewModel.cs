using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.Resources.Resx;
using Drive.Client.Validations;
using Drive.Client.Validations.ValidationRules;
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

        protected override void ResetValidationObjects() {
            base.ResetValidationObjects();

            MainInput.Validations.Add(new FullNameMinLengthRule<string>() { ValidationMessage = ResourceLoader.GetString(nameof(AppStrings.ERROR_MINLENGTH)) });
            MainInput.Validations.Add(new FullNameMaxLengthRule<string>() { ValidationMessage = ResourceLoader.GetString(nameof(AppStrings.ERROR_MAXLENGTH)) });
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is SearchByPersonArgs searchByPersonArgs) {
                _searchByPersonArgs = searchByPersonArgs;
            }

            MainInput.Value = string.Empty;

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
