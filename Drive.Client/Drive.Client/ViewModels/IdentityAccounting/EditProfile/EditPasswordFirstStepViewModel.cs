using Drive.Client.Models.Arguments.IdentityAccounting.ChangePassword;

namespace Drive.Client.ViewModels.IdentityAccounting.EditProfile {
    public sealed class EditPasswordFirstStepViewModel : IdentityAccountingStepBaseViewModel {

        /// <summary>
        ///     ctor().
        /// </summary>
        public EditPasswordFirstStepViewModel() {
            StepTitle = CURRENT_PASSWORD_STEP_REGISTRATION_TITLE;
            MainInputPlaceholder = PASSWORD_PLACEHOLDER_STEP_REGISTRATION;
            MainInputIconPath = PASSWORD_ICON_PATH;
            IsPasswordInput = true;
        }

        public override void Dispose() {
            base.Dispose();


        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                await NavigationService.NavigateToAsync<EditPasswordSecondStepViewModel>(new ChangePasswordArgs { CurrentPassword = MainInput.Value });
            }
        }
    }
}
