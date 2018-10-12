using Drive.Client.Exceptions;
using Drive.Client.Helpers;
using Drive.Client.Models.EntityModels.Identity;
using Drive.Client.Services.Identity;
using Drive.Client.Validations;
using Drive.Client.Validations.ValidationRules;
using Newtonsoft.Json;
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

            //StepTitle = CHANGE_PHONENUMBER_TITLE;
            MainInputPlaceholder = BaseSingleton<GlobalSetting>.Instance.UserProfile?.PhoneNumber; ;
            MainInputIconPath = PHONENUMBER_ICON_PATH;
            KeyboardType = Device.RuntimePlatform == Device.Android ? Keyboard.Telephone : Keyboard.Default;
        }

        public override void Dispose() {
            base.Dispose();

            ResetCancellationTokenSource(ref _changePhoneNumberCancellationTokenSource);
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                ResetCancellationTokenSource(ref _changePhoneNumberCancellationTokenSource);
                CancellationTokenSource cancellationTokenSource = _changePhoneNumberCancellationTokenSource;

                Guid busyKey = Guid.NewGuid();
                SetBusy(busyKey, true);
                try {
                    ChangedProfileData changedProfileData =  await _identityService.ChangePhoneNumberAsync(MainInput.Value, _changePhoneNumberCancellationTokenSource.Token);
                    if (changedProfileData != null) {
                        await NavigationService.PreviousPageViewModel.InitializeAsync(null);
                        await NavigationService.GoBackAsync();
                    }
                }
                catch (HttpRequestExceptionEx ex) {
                    var tt = JsonConvert.DeserializeObject<HttpRequestExceptionResult>(ex.Message);
                    ServerError = tt.Message;
                   
                    Debug.WriteLine($"ERROR:{tt.Message}");
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

        protected override void ResetValidationObjects() {
            base.ResetValidationObjects();

            MainInput.Validations.Add(new PhoneNumberRule<string>() { ValidationMessage = ValidatableObject<string>.INVALID_PHONE_VALIDATION_MESSAGE });
        }
    }
}
