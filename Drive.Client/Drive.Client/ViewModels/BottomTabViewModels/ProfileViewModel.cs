using Drive.Client.Helpers;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.Models.EntityModels.Identity;
using Drive.Client.Models.Medias;
using Drive.Client.Services;
using Drive.Client.Services.Identity;
using Drive.Client.Services.Media;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.BottomTabViewModels.Popups;
using Drive.Client.ViewModels.IdentityAccounting.EditProfile;
using Drive.Client.ViewModels.IdentityAccounting.Registration;
using Drive.Client.Views.BottomTabViews;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels {
    public sealed class ProfileViewModel : TabbedViewModelBase {

        private CancellationTokenSource _changeAvatarCancellationTokenSource = new CancellationTokenSource();

        private CancellationTokenSource _getUserCancellationTokenSource = new CancellationTokenSource();

        private readonly IPickMediaService _pickMediaService;

        private readonly IIdentityService _identityService;

        LanguageSelectPopupViewModel _languageSelectPopupViewModel;
        public LanguageSelectPopupViewModel LanguageSelectPopupViewModel {
            get => _languageSelectPopupViewModel;
            private set {
                _languageSelectPopupViewModel?.Dispose();
                SetProperty(ref _languageSelectPopupViewModel, value);
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

        public ICommand EditEmailCommand => new Command(async () => {
            await DependencyService.Get<Interface1>().GetPhotoAsync();
        });

        public ICommand ChangePasswordCommand => new Command(async () => await NavigationService.NavigateToAsync<EditPasswordFirstStepViewModel>());

        public ICommand LogOutCommand => new Command(async () => await _identityService.LogOutAsync());

        public ICommand ChangeAvatarCommand => new Command(async () => await OnChangeAvatarAsync());

        /// <summary>
        ///     ctor().
        /// </summary>
        public ProfileViewModel(IPickMediaService pickMediaService,
                                IIdentityService identityService) {
            _pickMediaService = pickMediaService;
            _identityService = identityService;

            LanguageSelectPopupViewModel = DependencyLocator.Resolve<LanguageSelectPopupViewModel>();
            LanguageSelectPopupViewModel.InitializeAsync(this);
        }

        private async void GetUser() {
            try {
                if (BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth) {
                    User user = await _identityService.GetUserAsync();

                    if (user != null) {
                        UpdateUserProfile(user);
                        UpdateView();
                    }
                }
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR:{ex.Message}");
                Debugger.Break();
            }
        }


        public override Task InitializeAsync(object navigationData) {
            UpdateView();

            if (navigationData is SelectedBottomBarTabArgs) {
                GetUser();
            }

            LanguageSelectPopupViewModel?.InitializeAsync(navigationData);

            return base.InitializeAsync(navigationData);
        }

        public override void Dispose() {
            base.Dispose();

            LanguageSelectPopupViewModel?.Dispose();
            ResetCancellationTokenSource(ref _changeAvatarCancellationTokenSource);
            ResetCancellationTokenSource(ref _getUserCancellationTokenSource);
        }

        protected override void TabbViewModelInit() {
            TabIcon = IconPath.PROFILE;
            RelativeViewType = typeof(ProfileView);
            HasBackgroundItem = false;
        }

        private static void UpdateUserProfile(User user) {
            BaseSingleton<GlobalSetting>.Instance.UserProfile.UserName = user.UserName;
            BaseSingleton<GlobalSetting>.Instance.UserProfile.PhoneNumber = user.PhoneNumber;
            BaseSingleton<GlobalSetting>.Instance.UserProfile.Email = user.Email;
            BaseSingleton<GlobalSetting>.Instance.UserProfile.AvatarUrl = user.AvatarUrl;
            BaseSingleton<GlobalSetting>.Instance.UserProfile.NetId = user.NetId;
        }

        private async Task OnChangeAvatarAsync() {
            ResetCancellationTokenSource(ref _changeAvatarCancellationTokenSource);
            CancellationTokenSource cancellationTokenSource = _changeAvatarCancellationTokenSource;

            try {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);
                if (status != PermissionStatus.Granted) {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Photos)) {
                        Debugger.Break();
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Photos);
                    if (results.ContainsKey(Permission.Photos))
                        status = results[Permission.Photos];
                }

                if (status == PermissionStatus.Granted) {
                    Guid busyKey = Guid.NewGuid();
                    UpdateBusyVisualState(busyKey, true);
                    try {
                        PickedImage pickedImage = await _pickMediaService.BuildPickedImageAsync();

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
                else if (status != PermissionStatus.Unknown) {
                    await DialogService.ShowAlertAsync("Photos access Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR:{ex.Message}");
                Debugger.Break();
            }
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
