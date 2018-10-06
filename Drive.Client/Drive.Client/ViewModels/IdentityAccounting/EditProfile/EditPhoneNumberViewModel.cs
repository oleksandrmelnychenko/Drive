using Drive.Client.Services.Identity;
using Drive.Client.Validations;
using Drive.Client.Validations.ValidationRules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.IdentityAccounting.EditProfile {
    public sealed class EditPhoneNumberViewModel : IdentityAccountingStepBaseViewModel {

        private readonly IIdentityService _identityService;

        private CancellationTokenSource _changePhoneNumberCancellationTokenSource = new CancellationTokenSource();

        /// <summary>
        ///     ctor().
        /// </summary>
        public EditPhoneNumberViewModel(IIdentityService identityService) {
            _identityService = identityService;

            StepTitle = CHANGE_PHONENUMBER_TITLE;
            MainInputPlaceholder = PHONE_PLACEHOLDER_STEP_REGISTRATION;
            MainInputIconPath = PHONENUMBER_ICON_PATH;
            KeyboardType = Device.RuntimePlatform == Device.Android ? Keyboard.Telephone : Keyboard.Default;
        }

        protected override void OnStepCommand() {
            if (ValidateForm()) {
                ResetCancellationTokenSource(ref _changePhoneNumberCancellationTokenSource);
                CancellationTokenSource cancellationTokenSource = _changePhoneNumberCancellationTokenSource;

                Guid busyKey = Guid.NewGuid();
                SetBusy(busyKey, true);
                try {

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
