using Drive.Client.Exceptions;
using Drive.Client.Models.Arguments.IdentityAccounting.Registration;
using Drive.Client.Models.EntityModels.Identity;
using Drive.Client.Services.Identity;
using Drive.Client.ViewModels.IdentityAccounting.ForgotPassword;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.IdentityAccounting.Registration {
    public sealed class SignInPasswordStepViewModel : StepBaseViewModel {

        private CancellationTokenSource _signInCancellationTokenSource = new CancellationTokenSource();

        private readonly IIdentityService _identityService;

        private SignInArgs _signInArgsArgs;

        public ICommand ForgotPasswordCommand => new Command(async () => await NavigationService.NavigateToAsync<ForgotPasswordFirstStepViewModel>(_signInArgsArgs.PhoneNumber));

        /// <summary>
        ///     ctor().
        /// </summary>
        public SignInPasswordStepViewModel(IIdentityService identityService) {
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

        public override void Dispose() {
            base.Dispose();

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
                        var signInResult = await _identityService.SignInAsync(_signInArgsArgs, cancellationTokenSource.Token);

                        if (signInResult != null) {
                            if (signInResult.IsSucceed) {
                                await NavigationService.InitializeAsync();
                            } else {
                                ServerError = signInResult.Errors.LastOrDefault().Item2;
                            }
                        } else {
                            Debugger.Break();
                            await NavigationService.GoBackAsync();
                        }
                    }
                    catch (HttpRequestExceptionEx ex) {
                        var tt = JsonConvert.DeserializeObject<HttpRequestExceptionResult>(ex.Message);
                        Debug.WriteLine($"ERROR:{tt.Message}");
                        ServerError = tt.Message;
                        Debugger.Break();
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
}
