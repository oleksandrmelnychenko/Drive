using Drive.Client.Models.Arguments.IdentityAccounting.ChangePassword;
using Drive.Client.Models.EntityModels.Identity;
using Drive.Client.Services.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.IdentityAccounting.EditProfile {
    public sealed class EditPasswordSecondStepViewModel : IdentityAccountingStepBaseViewModel {

        private ChangePasswordArgs _changePasswordArgs;

        /// <summary>
        ///     ctor().
        /// </summary>
        public EditPasswordSecondStepViewModel() {
            StepTitle = NEW_PASSWORD_STEP_REGISTRATION_TITLE;
            MainInputPlaceholder = PASSWORD_PLACEHOLDER_STEP_REGISTRATION;
            MainInputIconPath = PASSWORD_ICON_PATH;
            IsPasswordInput = true;
        }

        public override Task InitializeAsync(object navigationData) {
            if (navigationData is ChangePasswordArgs changePasswordArgs) {
                _changePasswordArgs = changePasswordArgs;
            }

            return base.InitializeAsync(navigationData);
        }

        public override void Dispose() {
            base.Dispose();            
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                if (_changePasswordArgs != null) {
                    _changePasswordArgs.NewPassword = MainInput.Value;
                    await NavigationService.NavigateToAsync<EditPasswordFinallyStepViewModel>(_changePasswordArgs);
                } else {
                    Debugger.Break();
                    await NavigationService.GoBackAsync();
                }
            }
        }
    }
}
