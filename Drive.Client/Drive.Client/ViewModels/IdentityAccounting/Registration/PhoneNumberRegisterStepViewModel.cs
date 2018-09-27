using Drive.Client.Models.Arguments.IdentityAccounting.Registration;
using Drive.Client.Validations;
using Drive.Client.Validations.ValidationRules;
using Xamarin.Forms;
using System;

namespace Drive.Client.ViewModels.IdentityAccounting.Registration {
    public class PhoneNumberRegisterStepViewModel : IdentityAccountingStepBaseViewModel {

        public PhoneNumberRegisterStepViewModel() {
            StepTitle = PHONE_STEP_REGISTRATION_TITLE;
            MainInputPlaceholder = PHONE_PLACEHOLDER_STEP_REGISTRATION;
            MainInputIconPath = PHONENUMBER_ICON_PATH;
            KeyboardType = Device.RuntimePlatform == Device.Android ? Keyboard.Telephone : Keyboard.Default;          
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                await NavigationService.NavigateToAsync<NameRegisterStepViewModel>(new RegistrationCollectedInputsArgs() { PhoneNumber = MainInput.Value });
            }
        }

        protected override void ResetValidationObjects() {
            base.ResetValidationObjects();

            MainInput.Validations.Add(new PhoneNumberRule<string>() { ValidationMessage = ValidatableObject<string>.INVALID_PHONE_VALIDATION_MESSAGE });
        }
    }
}
