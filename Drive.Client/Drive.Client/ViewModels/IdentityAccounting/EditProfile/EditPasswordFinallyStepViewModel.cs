using Drive.Client.Models.Arguments.IdentityAccounting.ChangePassword;
using Drive.Client.Models.EntityModels.Identity;
using Drive.Client.Services.Identity;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.IdentityAccounting.EditProfile {
    public sealed class EditPasswordFinallyStepViewModel : IdentityAccountingStepBaseViewModel {

        private CancellationTokenSource _updatePasswordCancellationTokenSource = new CancellationTokenSource();

        private ChangePasswordArgs _changePasswordArgs;

        private readonly IIdentityService _identityService;

        /// <summary>
        ///     ctor().
        /// </summary>
        /// <param name="identityService"></param>
        public EditPasswordFinallyStepViewModel(IIdentityService identityService) {
            _identityService = identityService;

            StepTitle = PASSWORD_CONFIRM_STEP_REGISTRATION_TITLE;
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

            ResetCancellationTokenSource(ref _updatePasswordCancellationTokenSource);
        }

        public override bool ValidateForm() {
            if (!base.ValidateForm()) return false;

            bool isValid = MainInput.Value != null && MainInput.Value.Equals(_changePasswordArgs.NewPassword);
            if (!isValid) {
                ServerError = "Повторно введений пароль невірний";
            }

            return isValid;
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                ResetCancellationTokenSource(ref _updatePasswordCancellationTokenSource);
                CancellationTokenSource cancellationTokenSource = _updatePasswordCancellationTokenSource;

                Guid busyKey = Guid.NewGuid();
                SetBusy(busyKey, true);

                
                try {
                    User user = await _identityService.UpdatePasswordAsync(_changePasswordArgs, _updatePasswordCancellationTokenSource.Token);

                    if (user != null) {
                        await NavigationService.InitializeAsync();
                    }
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR:{ex.Message}");
                    ServerError = ex.Message;
                    Debugger.Break();
                }
                SetBusy(busyKey, false);
            }
        }
    }
}
