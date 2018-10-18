using Drive.Client.Extensions;
using Drive.Client.Models.Arguments.IdentityAccounting.Registration;
using Drive.Client.Validations;
using Drive.Client.Validations.ValidationRules;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.IdentityAccounting.Registration {
    public sealed class SignInPhoneNumberStepViewModel : StepBaseViewModel {

        /// <summary>
        ///     ctor().
        /// </summary>
        public SignInPhoneNumberStepViewModel() {
            StepTitle = PHONENUMBER_TITLE;
            MainInputPlaceholder = PHONE_PLACEHOLDER_STEP_REGISTRATION;
            MainInputIconPath = PHONENUMBER_ICON_PATH;
            KeyboardType = Device.RuntimePlatform == Device.Android ? Keyboard.Telephone : Keyboard.Default;
        }

        public override void Dispose() {
            base.Dispose();

        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                Guid busyKey = Guid.NewGuid();
                SetBusy(busyKey, true);

                try {
                    await NavigationService.NavigateToAsync<SignInPasswordStepViewModel>(new SignInArgs() { PhoneNumber = MainInput.Value });
                }
                catch (Exception ex) {
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
