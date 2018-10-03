using Drive.Client.Models.Arguments.IdentityAccounting.Registration;
using Drive.Client.Validations;
using Drive.Client.Validations.ValidationRules;
using Xamarin.Forms;
using System;
using Drive.Client.Services.EventStore;
using System.Diagnostics;
using Drive.Client.Services.Identity;
using System.Threading;
using Drive.Client.Models.EntityModels.Identity;

namespace Drive.Client.ViewModels.IdentityAccounting.Registration {
    public class PhoneNumberRegisterStepViewModel : IdentityAccountingStepBaseViewModel {

        private const string DRIVE_AUTO_STREAM = "DriveAutoStream";

        private CancellationTokenSource _checkPhoneNumberCancellationTokenSource = new CancellationTokenSource();

        private readonly IIdentityService _identityService;

        /// <summary>
        ///     ctor().
        /// </summary>
        /// <param name="eventStoreService"></param>
        public PhoneNumberRegisterStepViewModel(IIdentityService identityService) {
            _identityService = identityService;

            StepTitle = PHONE_STEP_REGISTRATION_TITLE;
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
                try {
                    ResetCancellationTokenSource(ref _checkPhoneNumberCancellationTokenSource);
                    CancellationTokenSource cancellationTokenSource = _checkPhoneNumberCancellationTokenSource;

                    PhoneNumberAvailabilty phoneNumberAvailabilty =  await _identityService.CheckPhoneNumberAvailabiltyAsync(MainInput.Value, cancellationTokenSource.Token);

                    if (phoneNumberAvailabilty != null && phoneNumberAvailabilty.IsRequestSuccess) {
                        await NavigationService.NavigateToAsync<NameRegisterStepViewModel>(new RegistrationCollectedInputsArgs() { PhoneNumber = MainInput.Value });
                    } else {
                        // output error message
                    }
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR:{ex.Message}");
                    Debugger.Break();
                }
            }
        }

        protected override void ResetValidationObjects() {
            base.ResetValidationObjects();

            MainInput.Validations.Add(new PhoneNumberRule<string>() { ValidationMessage = ValidatableObject<string>.INVALID_PHONE_VALIDATION_MESSAGE });
        }
    }
}
