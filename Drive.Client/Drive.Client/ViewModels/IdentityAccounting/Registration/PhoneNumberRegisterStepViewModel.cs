using Drive.Client.Models.Arguments.IdentityAccounting.Registration;
using Drive.Client.Models.EntityModels.Identity;
using Drive.Client.Services.Identity;
using Drive.Client.Validations;
using Drive.Client.Validations.ValidationRules;
using System;
using System.Diagnostics;
using System.Threading;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.IdentityAccounting.Registration {
    public class PhoneNumberRegisterStepViewModel : IdentityAccountingStepBaseViewModel {

        private CancellationTokenSource _checkPhoneNumberCancellationTokenSource = new CancellationTokenSource();

        private readonly IIdentityService _identityService;

        /// <summary>
        ///     ctor().
        /// </summary>
        /// <param name="eventStoreService"></param>
        public PhoneNumberRegisterStepViewModel(IIdentityService identityService) {
            _identityService = identityService;

            StepTitle = PHONENUMBER_TITLE;
            MainInputPlaceholder = PHONE_PLACEHOLDER_STEP_REGISTRATION;
            MainInputIconPath = PHONENUMBER_ICON_PATH;
            KeyboardType = Device.RuntimePlatform == Device.Android ? Keyboard.Telephone : Keyboard.Default;
        }

        public override void Dispose() {
            base.Dispose();

            ResetCancellationTokenSource(ref _checkPhoneNumberCancellationTokenSource);
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {

                Guid busyKey = Guid.NewGuid();
                SetBusy(busyKey, true);

                try {
                    ResetCancellationTokenSource(ref _checkPhoneNumberCancellationTokenSource);
                    CancellationTokenSource cancellationTokenSource = _checkPhoneNumberCancellationTokenSource;

                    PhoneNumberAvailabilty phoneNumberAvailabilty = await _identityService.CheckPhoneNumberAvailabiltyAsync(MainInput.Value, cancellationTokenSource.Token);

                    if (phoneNumberAvailabilty != null) {
                        if (phoneNumberAvailabilty.IsRequestSuccess) {
                            await NavigationService.NavigateToAsync<NameRegisterStepViewModel>(new RegistrationCollectedInputsArgs() { PhoneNumber = MainInput.Value });
                        } else {
                            ServerError = phoneNumberAvailabilty.Message;
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

        protected override void ResetValidationObjects() {
            base.ResetValidationObjects();

            MainInput.Validations.Add(new PhoneNumberRule<string>() { ValidationMessage = ValidatableObject<string>.INVALID_PHONE_VALIDATION_MESSAGE });
        }
    }
}
