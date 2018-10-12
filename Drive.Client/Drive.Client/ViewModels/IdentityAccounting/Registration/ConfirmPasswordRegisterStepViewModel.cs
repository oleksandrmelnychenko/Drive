using Drive.Client.Models.Arguments.IdentityAccounting.Registration;
using Drive.Client.Models.EntityModels.Identity;
using Drive.Client.Services.Identity;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.IdentityAccounting.Registration {
    public class ConfirmPasswordRegisterStepViewModel : IdentityAccountingStepBaseViewModel {

        private CancellationTokenSource _signUpCancellationTokenSource = new CancellationTokenSource();

        private RegistrationCollectedInputsArgs _collectedInputsArgs;

        private readonly IIdentityService _identityService;

        /// <summary>
        ///     ctor().
        /// </summary>
        public ConfirmPasswordRegisterStepViewModel(IIdentityService identityService) {
            _identityService = identityService;

            //StepTitle = PASSWORD_CONFIRM_STEP_REGISTRATION_TITLE;
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

        }

        public override bool ValidateForm() {
            if (!base.ValidateForm()) return false;

            bool isValid = MainInput.Value != null && MainInput.Value.Equals(_collectedInputsArgs.Password);
            if (!isValid) {
                ServerError = "Повторно введений пароль невірний";
            }

            return isValid;
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                if (_collectedInputsArgs != null) {
                    ResetCancellationTokenSource(ref _signUpCancellationTokenSource);
                    CancellationTokenSource cancellationTokenSource = _signUpCancellationTokenSource;

                    Guid busyKey = Guid.NewGuid();
                    SetBusy(busyKey, true);

                    try {
                        AuthenticationResult signUpResult = await _identityService.SignUpAsync(_collectedInputsArgs, cancellationTokenSource.Token);

                        if (signUpResult != null) {
                            if (signUpResult.IsSucceed) {
                                await NavigationService.InitializeAsync();
                            } else {
                                ServerError = signUpResult.Errors.LastOrDefault().Item2;
                            }
                        } else {
                            Debugger.Break();
                            await NavigationService.GoBackAsync();
                        }
                    }
                    catch (Exception ex) {
                        Debug.WriteLine($"ERROR:{ex.Message}");
                        Debugger.Break();
                    }
                    SetBusy(busyKey, false);
                }
            }
        }
    }
}
