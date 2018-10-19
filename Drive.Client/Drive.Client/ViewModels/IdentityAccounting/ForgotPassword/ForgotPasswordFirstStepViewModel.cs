using Drive.Client.Models.Arguments.IdentityAccounting.ForgotPassword;
using Drive.Client.Models.EntityModels.Identity;
using Drive.Client.Services.Identity;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.IdentityAccounting.ForgotPassword {
    public sealed class ForgotPasswordFirstStepViewModel: StepBaseViewModel {

        private CancellationTokenSource _canUserChangeForgottenPasswordCancellationTokenSource = new CancellationTokenSource();

        private readonly IIdentityService _identityService;

        private ForgotPasswordArgs _forgotPasswordArgs;

        /// <summary>
        ///     ctor().
        /// </summary>
        public ForgotPasswordFirstStepViewModel(IIdentityService identityService) {
            _identityService = identityService;

            StepTitle = NAME_STEP_REGISTRATION_TITLE;
            MainInputPlaceholder = NAME_PLACEHOLDER_STEP_REGISTRATION;
            MainInputIconPath = NAME_ICON_PATH;
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is string) {
                _forgotPasswordArgs = new ForgotPasswordArgs { PhoneNumber = navigationData as string };
            }

            MainInput.Value = string.Empty;

            return base.InitializeAsync(navigationData);
        }

        public override void Dispose() {
            base.Dispose();

            ResetCancellationTokenSource(ref _canUserChangeForgottenPasswordCancellationTokenSource);
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {

                Guid busyKey = Guid.NewGuid();
                SetBusy(busyKey, true);

                try {
                    ResetCancellationTokenSource(ref _canUserChangeForgottenPasswordCancellationTokenSource);
                    CancellationTokenSource cancellationTokenSource = _canUserChangeForgottenPasswordCancellationTokenSource;

                    CanChangeForgottenPassword canChangeForgottenPassword = null;

                    canChangeForgottenPassword = await _identityService.CanUserChangeForgottenPasswordAsync(_forgotPasswordArgs?.PhoneNumber, MainInput.Value, cancellationTokenSource.Token);

                    if (canChangeForgottenPassword != null) {
                        if (canChangeForgottenPassword.IsRequestSuccess) {
                            _forgotPasswordArgs.UserName = MainInput.Value;
                            await NavigationService.NavigateToAsync<ForgotPasswordSecondStepViewModel>(_forgotPasswordArgs);
                        } else {
                            ServerError = canChangeForgottenPassword.Message;
                        }
                    }
                }
                catch (Exception ex) {
                    ServerError = ex.Message;
                    Debug.WriteLine($"ERROR:{ex.Message}");
                    Debugger.Break();
                }
                SetBusy(busyKey, false);
            }
        }
    }
}
