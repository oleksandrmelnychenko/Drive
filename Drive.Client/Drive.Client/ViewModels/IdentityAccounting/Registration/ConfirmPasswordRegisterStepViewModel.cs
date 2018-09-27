using Drive.Client.Models.Arguments.IdentityAccounting.Registration;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.IdentityAccounting.Registration {
    public class ConfirmPasswordRegisterStepViewModel : IdentityAccountingStepBaseViewModel {

        private RegistrationCollectedInputsArgs _collectedInputsArgs;

        public ConfirmPasswordRegisterStepViewModel() {
            StepTitle = PASSWORD_CONFIRM_STEP_REGISTRATION_TITLE;
            MainInputPlaceholder = PASSWORD_PLACEHOLDER_STEP_REGISTRATION;
            MainInputIconPath = PASSWORD_ICON_PATH;
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
                _collectedInputsArgs.ConfirmedPassword = null;
            }
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                if (_collectedInputsArgs != null) {
                    await DialogService.ToastAsync(string.Format("TODO: In developing. Check password matchings. {0} {1} {2}", _collectedInputsArgs.PhoneNumber, _collectedInputsArgs.Name, _collectedInputsArgs.Password));
                }
                else {
                    Debugger.Break();
                    await NavigationService.GoBackAsync();
                }
            }
        }
    }
}
