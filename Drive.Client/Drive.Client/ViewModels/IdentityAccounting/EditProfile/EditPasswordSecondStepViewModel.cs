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

        private CancellationTokenSource _updatePasswordCancellationTokenSource = new CancellationTokenSource();

        private ChangePasswordArgs _changePasswordArgs;

        private readonly IIdentityService _identityService;

        public string CurrentPassword { get; set; }

        /// <summary>
        ///     ctor().
        /// </summary>
        public EditPasswordSecondStepViewModel(IIdentityService identityService) {
            _identityService = identityService;

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

            ResetCancellationTokenSource(ref _updatePasswordCancellationTokenSource);
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                ResetCancellationTokenSource(ref _updatePasswordCancellationTokenSource);
                CancellationTokenSource cancellationTokenSource = _updatePasswordCancellationTokenSource;

                Guid busyKey = Guid.NewGuid();
                SetBusy(busyKey, true);

                _changePasswordArgs.NewPassword = MainInput.Value;
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
