using Drive.Client.Exceptions;
using Drive.Client.Helpers;
using Drive.Client.Models.EntityModels.Identity;
using Drive.Client.Resources.Resx;
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
    public sealed class EditPhoneNumberViewModel : StepBaseViewModel {

        private readonly IIdentityService _identityService;

        private CancellationTokenSource _changePhoneNumberCancellationTokenSource = new CancellationTokenSource();

        private string _currentPhoneNumber;
        public string CurrentPhoneNumber {
            get { return _currentPhoneNumber; }
            set { SetProperty(ref _currentPhoneNumber, value); }
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public EditPhoneNumberViewModel(IIdentityService identityService) {
            _identityService = identityService;

            StepTitle = CHANGE_PHONENUMBER_TITLE;
            CurrentPhoneNumber = BaseSingleton<GlobalSetting>.Instance.UserProfile?.PhoneNumber; ;
            MainInputIconPath = PHONENUMBER_ICON_PATH;
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
                        await NavigationService.PreviousPageViewModel?.InitializeAsync(null);
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

            MainInput.Validations.Add(new PhoneNumberRule<string>() { ValidationMessage = ResourceLoader.GetString(nameof(AppStrings.InvalidPhoneNumber)) });
        }
    }
}
