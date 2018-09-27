using System.Diagnostics;
using System.Threading.Tasks;
using Drive.Client.Models.Arguments.IdentityAccounting.Registration;

namespace Drive.Client.ViewModels.IdentityAccounting.Registration {
    class NameRegisterStepViewModel : IdentityAccountingStepBaseViewModel {

        private RegistrationCollectedInputsArgs _collectedInputsArgs;

        public NameRegisterStepViewModel() {
            StepTitle = NAME_STEP_REGISTRATION_TITLE;
            MainInputPlaceholder = NAME_PLACEHOLDER_STEP_REGISTRATION;
            MainInputIconPath = NAME_ICON_PATH;
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is RegistrationCollectedInputsArgs collectedInputsArgs) {
                _collectedInputsArgs = collectedInputsArgs;
            }

            return base.InitializeAsync(navigationData);
        }

        public override void Dispose() {
            base.Dispose();

            if (_collectedInputsArgs != null) {
                _collectedInputsArgs.Name = null;
            }
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                if (_collectedInputsArgs != null) {
                    _collectedInputsArgs.Name = MainInput.Value;
                    await NavigationService.NavigateToAsync<PasswordRegisterStepViewModel>(_collectedInputsArgs);
                }
                else {
                    Debugger.Break();
                    await NavigationService.GoBackAsync();
                }
            }
        }
    }
}
