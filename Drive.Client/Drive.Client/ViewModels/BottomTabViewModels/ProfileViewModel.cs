using Drive.Client.Helpers;
using Drive.Client.Models.Medias;
using Drive.Client.Services.Identity;
using Drive.Client.Services.Identity.IdentityUtility;
using Drive.Client.Services.Media;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.BottomTabViewModels.Popups;
using Drive.Client.ViewModels.IdentityAccounting.EditProfile;
using Drive.Client.ViewModels.IdentityAccounting.Registration;
using Drive.Client.Views.BottomTabViews;
using Plugin.Media.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels {
    public sealed class ProfileViewModel : NestedViewModel, IBottomBarTab {

        private CancellationTokenSource _changeAvatarCancellationTokenSource = new CancellationTokenSource();

        private readonly IIdentityUtilityService _identityUtilityService;

        private readonly IPickMediaService _pickMediaService;

        private readonly IIdentityService _identityService;

        public bool IsBudgeVisible { get; private set; }

        public int BudgeCount { get; private set; }

        public string TabHeader { get; private set; }

        public string TabIcon { get; private set; } = IconPath.PROFILE;

        public Type RelativeViewType { get; private set; } = typeof(ProfileView);

        public bool HasBackgroundItem => false;

        LanguageSelectPopupViewModel _languageSelectPopupViewModel;
        public LanguageSelectPopupViewModel LanguageSelectPopupViewModel {
            get => _languageSelectPopupViewModel;
            private set {
                _languageSelectPopupViewModel?.Dispose();
                SetProperty<LanguageSelectPopupViewModel>(ref _languageSelectPopupViewModel, value);
            }
        }

        bool _visibilityRegistrationContent;
        public bool VisibilityRegistrationContent {
            get { return _visibilityRegistrationContent; }
            set { SetProperty(ref _visibilityRegistrationContent, value); }
        }

        string _userName;
        public string UserName {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        string _phoneNumber;
        public string PhoneNumber {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
        }

        string _email;
        public string Email {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        string _avatarUrl;
        public string AvatarUrl {
            get { return _avatarUrl; }
            set { SetProperty(ref _avatarUrl, value); }
        }

        public ICommand FacebookLoginCommand => new Command(async () => await DialogService.ToastAsync("Facebook login command in developing"));

        public ICommand LoginCommand => new Command(async () => await NavigationService.NavigateToAsync<SignInPhoneNumberStepViewModel>());

        public ICommand RegisterCommand => new Command(async () => await NavigationService.NavigateToAsync<PhoneNumberRegisterStepViewModel>());

        public ICommand EditPhoneNumberCommand => new Command(async () => await NavigationService.NavigateToAsync<EditPhoneNumberViewModel>());

        public ICommand EditUserNameCommand => new Command(async () => await NavigationService.NavigateToAsync<EditUserNameViewModel>());

        public ICommand EditEmailCommand => new Command(async () => await NavigationService.NavigateToAsync<EditEmailViewModel>());

        public ICommand LogOutCommand => new Command(async () => await OnLogOutAsync());
       
        public ICommand ChangeAvatarCommand => new Command(async () => await OnChangeAvatarAsync());

        /// <summary>
        ///     ctor().
        /// </summary>
        public ProfileViewModel(IIdentityUtilityService identityUtilityService,
                                IPickMediaService pickMediaService,
                                IIdentityService identityService) {
            _identityUtilityService = identityUtilityService;
            _pickMediaService = pickMediaService;
            _identityService = identityService;

            LanguageSelectPopupViewModel = DependencyLocator.Resolve<LanguageSelectPopupViewModel>();
            LanguageSelectPopupViewModel.InitializeAsync(this);
        }

        public override Task InitializeAsync(object navigationData) {
            UpdateView();

            LanguageSelectPopupViewModel?.InitializeAsync(navigationData);

            return base.InitializeAsync(navigationData);
        }

        public override void Dispose() {
            base.Dispose();

            LanguageSelectPopupViewModel?.Dispose();
            ResetCancellationTokenSource(ref _changeAvatarCancellationTokenSource);
        }

        private async Task OnLogOutAsync() {
            Guid busyKey = Guid.NewGuid();
            UpdateBusyVisualState(busyKey, true);
            await _identityUtilityService.LogOutAsync();
            UpdateBusyVisualState(busyKey, false);
        }

        private async Task OnChangeAvatarAsync() {
            ResetCancellationTokenSource(ref _changeAvatarCancellationTokenSource);
            CancellationTokenSource cancellationTokenSource = _changeAvatarCancellationTokenSource;

            Guid busyKey = Guid.NewGuid();
            UpdateBusyVisualState(busyKey, true);
            try {
                PickedImage pickedImage = null;

                using (MediaFile file = await _pickMediaService.PickPhotoAsync()) {
                    pickedImage = await _pickMediaService.BuildPickedImageAsync(file);
                }

                string avatarUrl = await _identityService.UploadUserAvatarAsync(pickedImage, _changeAvatarCancellationTokenSource.Token);

                if (!string.IsNullOrEmpty(avatarUrl)) {
                    AvatarUrl = avatarUrl;
                }
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR:{ex.Message}");
                Debugger.Break();
            }

            UpdateBusyVisualState(busyKey, false);
        }


        private void UpdateView() {
            try {
                VisibilityRegistrationContent = !BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth;
                UserName = BaseSingleton<GlobalSetting>.Instance.UserProfile?.UserName;
                PhoneNumber = BaseSingleton<GlobalSetting>.Instance.UserProfile?.PhoneNumber;
                Email = BaseSingleton<GlobalSetting>.Instance.UserProfile?.Email;
                AvatarUrl = BaseSingleton<GlobalSetting>.Instance.UserProfile.AvatarUrl;
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR:{ex.Message}");
                Debugger.Break();
            }
        }
    }
}
