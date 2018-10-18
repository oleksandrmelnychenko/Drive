using Drive.Client.Models.Arguments.IdentityAccounting.ForgotPassword;
using Drive.Client.Services.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.IdentityAccounting.ForgotPassword {
    public sealed class ForgotPasswordSecondStepViewModel : StepBaseViewModel {

        private ForgotPasswordArgs _forgotPasswordArgs;

        /// <summary>
        ///     ctor().
        /// </summary>
        public ForgotPasswordSecondStepViewModel() {
            StepTitle = NEW_PASSWORD_STEP_REGISTRATION_TITLE;
            MainInputPlaceholder = PASSWORD_PLACEHOLDER_STEP_REGISTRATION;
            MainInputIconPath = PASSWORD_ICON_PATH;
            IsPasswordInput = true;
        }

        public override void Dispose() {
            base.Dispose();
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is ForgotPasswordArgs forgotPasswordArgs) {
                _forgotPasswordArgs = forgotPasswordArgs;
            }

            return base.InitializeAsync(navigationData);
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                if (_forgotPasswordArgs != null) {
                    _forgotPasswordArgs.NewPassword = MainInput.Value;

                    await NavigationService.NavigateToAsync<ForgotPasswordFinallyStepViewModel>(_forgotPasswordArgs);
                }
            }
        }
    }
}
