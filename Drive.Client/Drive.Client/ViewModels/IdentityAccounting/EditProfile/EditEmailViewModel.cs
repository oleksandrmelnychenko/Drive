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

namespace Drive.Client.ViewModels.IdentityAccounting.EditProfile {
    public sealed class EditEmailViewModel : IdentityAccountingStepBaseViewModel {

        private readonly IIdentityService _identityService;

        private CancellationTokenSource _changeEmailCancellationTokenSource = new CancellationTokenSource();

        /// <summary>
        ///     ctor().
        /// </summary>
        public EditEmailViewModel(IIdentityService identityService) {
            _identityService = identityService;

            //StepTitle = CHANGE_EMAIL_TITLE;
            MainInputPlaceholder = BaseSingleton<GlobalSetting>.Instance.UserProfile?.Email;
            MainInputIconPath = EMAIL_ICON_PATH;
        }

        public override void Dispose() {
            base.Dispose();

            ResetCancellationTokenSource(ref _changeEmailCancellationTokenSource);
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                ResetCancellationTokenSource(ref _changeEmailCancellationTokenSource);
                CancellationTokenSource cancellationTokenSource = _changeEmailCancellationTokenSource;

                Guid busyKey = Guid.NewGuid();
                SetBusy(busyKey, true);
                try {
                    ChangedProfileData changedProfileData = await _identityService.ChangeEmailAsync(MainInput.Value, _changeEmailCancellationTokenSource.Token);
                    if (changedProfileData != null) {
                        await NavigationService.PreviousPageViewModel.InitializeAsync(null);
                        await NavigationService.GoBackAsync();
                    }
                }
                catch (HttpRequestExceptionEx ex) {
                    try {
                        var error = JsonConvert.DeserializeObject<HttpRequestExceptionResult>(ex.Message);
                        ServerError = error.Message;

                        Debug.WriteLine($"ERROR:{error.Message}");
                        Debugger.Break();

                    }
                    catch (Exception) {
                        Debugger.Break();
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

            MainInput.Validations.Add(new EmailRule<string>() { ValidationMessage = ValidatableObject<string>.ERROR_EMAIL });
        }
    }
}
