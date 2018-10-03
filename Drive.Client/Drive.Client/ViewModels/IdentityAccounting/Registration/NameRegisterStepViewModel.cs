using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Drive.Client.Models.Arguments.IdentityAccounting.Registration;
using Drive.Client.Models.EntityModels.Identity;
using Drive.Client.Services.Identity;

namespace Drive.Client.ViewModels.IdentityAccounting.Registration {
    class NameRegisterStepViewModel : IdentityAccountingStepBaseViewModel {

        private RegistrationCollectedInputsArgs _collectedInputsArgs;

        private CancellationTokenSource _checkUserNameCancellationTokenSource = new CancellationTokenSource();

        private readonly IIdentityService _identityService;

        public NameRegisterStepViewModel(IIdentityService identityService) {
            _identityService = identityService;

            StepTitle = NAME_STEP_REGISTRATION_TITLE;
            MainInputPlaceholder = NAME_PLACEHOLDER_STEP_REGISTRATION;
            MainInputIconPath = NAME_ICON_PATH;
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is RegistrationCollectedInputsArgs collectedInputsArgs) {
                _collectedInputsArgs = collectedInputsArgs;
            }

            return base.InitializeAsync(navigationData);
        }

        public override void Dispose() {
            base.Dispose();

            if (_collectedInputsArgs != null) {
                _collectedInputsArgs.Name = null;
            }

            ResetCancellationTokenSource(ref _checkUserNameCancellationTokenSource);
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                if (_collectedInputsArgs != null) {
                    ResetCancellationTokenSource(ref _checkUserNameCancellationTokenSource);
                    CancellationTokenSource cancellationTokenSource = _checkUserNameCancellationTokenSource;

                    UserNameAvailability userNameAvailability = await _identityService.CheckUserNameAvailabiltyAsync(MainInput.Value, cancellationTokenSource.Token);

                    if (userNameAvailability != null && userNameAvailability.IsRequestSuccess) {
                        _collectedInputsArgs.Name = MainInput.Value;
                        await NavigationService.NavigateToAsync<PasswordRegisterStepViewModel>(_collectedInputsArgs);
                    } else {
                        //error.
                    }
                } else {
                    Debugger.Break();
                    await NavigationService.GoBackAsync();
                }
            }
        }
    }
}
