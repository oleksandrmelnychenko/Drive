using Drive.Client.Models.Arguments.IdentityAccounting.Registration;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.IdentityAccounting.Registration {
    class PasswordRegisterStepViewModel: IdentityAccountingStepBaseViewModel {

        private RegistrationCollectedInputsArgs _collectedInputsArgs;

        public PasswordRegisterStepViewModel() {
            StepTitle = PASSWORD_STEP_REGISTRATION_TITLE;
            MainInputPlaceholder = PASSWORD_PLACEHOLDER_STEP_REGISTRATION;
            MainInputIconPath = TODO_INPUT_ICON_STUB;
            IsPasswordInput = true;
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
                _collectedInputsArgs.Password = null;
            }
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                if (_collectedInputsArgs != null) {
                    _collectedInputsArgs.Password = MainInput.Value;
                    await NavigationService.NavigateToAsync<ConfirmPasswordRegisterStepViewModel>(_collectedInputsArgs);
                }
                else {
                    Debugger.Break();
                    await NavigationService.GoBackAsync();
                }
            }
        }
    }
}
