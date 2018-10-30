using Drive.Client.Exceptions;
using Drive.Client.Helpers.Localize;
using Drive.Client.Models.Arguments.IdentityAccounting.ChangePassword;
using Drive.Client.Models.EntityModels.Identity;
using Drive.Client.Services.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Drive.Client.ViewModels.IdentityAccounting.EditProfile {
    public sealed class EditPasswordFirstStepViewModel : StepBaseViewModel {

        private readonly IIdentityService _identityService;

        private CancellationTokenSource _isCurrentPasswordExistCancellationTokenSource = new CancellationTokenSource();

        /// <summary>
        ///     ctor().
        /// </summary>
        public EditPasswordFirstStepViewModel(IIdentityService identityService) {
            _identityService = identityService;

            StepTitle = CURRENT_PASSWORD_STEP_REGISTRATION_TITLE;
            MainInputPlaceholder = PASSWORD_PLACEHOLDER_STEP_REGISTRATION;
            MainInputIconPath = PASSWORD_ICON_PATH;
            IsPasswordInput = true;
        }

        public override void Dispose() {
            base.Dispose();

            ResetCancellationTokenSource(ref _isCurrentPasswordExistCancellationTokenSource);
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                Guid busyKey = Guid.NewGuid();
                SetBusy(busyKey, true);

                ResetCancellationTokenSource(ref _isCurrentPasswordExistCancellationTokenSource);
                CancellationTokenSource cancellationTokenSource = _isCurrentPasswordExistCancellationTokenSource;

                try {
                    IsCurrentPasswordExistResponse isValidPasswordExist = await _identityService.IsCurrentPasswordExistAsync(MainInput.Value, cancellationTokenSource.Token);

                    SetBusy(busyKey, false);

                    if (isValidPasswordExist.IsRequestSuccess && (bool)isValidPasswordExist.Data) {
                        await NavigationService.NavigateToAsync<EditPasswordSecondStepViewModel>(new ChangePasswordArgs { CurrentPassword = MainInput.Value });
                    }
                    else {
                        ServerError = isValidPasswordExist.Message;
                    }
                }
                catch (OperationCanceledException) { }
                catch (ObjectDisposedException) { }
                catch (ServiceAuthenticationException) { }
                catch (System.Exception) {
                    Debugger.Break();

                    SetBusy(busyKey, false);
                }
            }
        }
    }
}
