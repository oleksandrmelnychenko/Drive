using Drive.Client.Exceptions;
using Drive.Client.Helpers;
using Drive.Client.Models.EntityModels.Identity;
using Drive.Client.Services.Identity;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading;

namespace Drive.Client.ViewModels.IdentityAccounting.EditProfile {
    public sealed class EditUserNameViewModel : StepBaseViewModel {

        private readonly IIdentityService _identityService;

        private CancellationTokenSource _changeUserNameCancellationTokenSource = new CancellationTokenSource();

        string _currentNamePlaceholder;
        public string CurrentNamePlaceholder {
            get { return _currentNamePlaceholder; }
            set { SetProperty(ref _currentNamePlaceholder, value); }
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public EditUserNameViewModel(IIdentityService identityService) {
            _identityService = identityService;

            StepTitle = NAME_STEP_REGISTRATION_TITLE;
            CurrentNamePlaceholder = BaseSingleton<GlobalSetting>.Instance.UserProfile?.UserName;
            MainInputIconPath = NAME_ICON_PATH;
        }

        public override void Dispose() {
            base.Dispose();

            ResetCancellationTokenSource(ref _changeUserNameCancellationTokenSource);
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                ResetCancellationTokenSource(ref _changeUserNameCancellationTokenSource);
                CancellationTokenSource cancellationTokenSource = _changeUserNameCancellationTokenSource;

                Guid busyKey = Guid.NewGuid();
                SetBusy(busyKey, true);
                try {
                    ChangedProfileData changedProfileData = await _identityService.ChangeUserNameAsync(MainInput.Value, _changeUserNameCancellationTokenSource.Token);
                    if (changedProfileData != null) {
                        await NavigationService.PreviousPageViewModel.InitializeAsync(null);
                        await NavigationService.GoBackAsync();
                    }
                }
                catch (HttpRequestExceptionEx ex) {
                    var tt = JsonConvert.DeserializeObject<HttpRequestExceptionResult>(ex.Message);
                    await DialogService.ToastAsync(tt.Message);
                    Debug.WriteLine($"ERROR:{tt.Message}");
                    Debugger.Break();
                }
                catch (Exception ex) {
                    await DialogService.ToastAsync(ex.Message);
                    Debug.WriteLine($"ERROR:{ex.Message}");
                    Debugger.Break();
                }
                SetBusy(busyKey, false);
            }
        }
    }
}
