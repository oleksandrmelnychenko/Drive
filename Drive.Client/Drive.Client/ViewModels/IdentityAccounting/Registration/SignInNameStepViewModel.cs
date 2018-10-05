using Drive.Client.Models.Arguments.IdentityAccounting.Registration;
using Drive.Client.Services.Identity;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.IdentityAccounting.Registration {
    public sealed class SignInNameStepViewModel : IdentityAccountingStepBaseViewModel {

        private CancellationTokenSource _signInCancellationTokenSource = new CancellationTokenSource();

        private readonly IIdentityService _identityService;

        private SignInArgs _signInArgsArgs;

        /// <summary>
        ///     ctor().
        /// </summary>
        public SignInNameStepViewModel(IIdentityService identityService) {
            _identityService = identityService;

            StepTitle = PASSWORD_STEP_REGISTRATION_TITLE;
            MainInputPlaceholder = PASSWORD_PLACEHOLDER_STEP_REGISTRATION;
            MainInputIconPath = PASSWORD_ICON_PATH;
            IsPasswordInput = true;
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is SignInArgs signInArgs) {
                _signInArgsArgs = signInArgs;
            }

            return base.InitializeAsync(navigationData);
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                if (_signInArgsArgs != null) {
                    _signInArgsArgs.Password = MainInput.Value;

                    ResetCancellationTokenSource(ref _signInCancellationTokenSource);
                    CancellationTokenSource cancellationTokenSource = _signInCancellationTokenSource;

                    Guid busyKey = Guid.NewGuid();
                    SetBusy(busyKey, true);

                    try {
                        var signUpResult = await _identityService.SignInAsync(_signInArgsArgs, cancellationTokenSource.Token);

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
