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

        private CancellationTokenSource _getUserCancellationTokenSource = new CancellationTokenSource();

        private readonly IPickMediaService _pickMediaService;

        private readonly IIdentityService _identityService;       

        bool _visibilityRegistrationContent;
        public bool VisibilityRegistrationContent {
            get { return _visibilityRegistrationContent; }
            set { SetProperty(ref _visibilityRegistrationContent, value); }
        }   

        string _avatarUrl;
        public string AvatarUrl {
            get { return _avatarUrl; }
            set { SetProperty(ref _avatarUrl, value); }
        }

        public ICommand LoginCommand => new Command(async () => await NavigationService.NavigateToAsync<SignInPhoneNumberStepViewModel>());

        public ICommand RegisterCommand => new Command(async () => await NavigationService.NavigateToAsync<PhoneNumberRegisterStepViewModel>());

        public ICommand SettingsCommand => new Command(async () => await NavigationService.NavigateToAsync<SettingsViewModel>());
        
        /// <summary>
        ///     ctor().
        /// </summary>
        public ProfileViewModel(IPickMediaService pickMediaService,
                                IIdentityService identityService) {
            _pickMediaService = pickMediaService;
            _identityService = identityService;

           
        }

        public override Task InitializeAsync(object navigationData) {
            UpdateView();

            if (navigationData is SelectedBottomBarTabArgs) {
            }

            return base.InitializeAsync(navigationData);
        }

        public override void Dispose() {
            base.Dispose();

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

        private void UpdateView() {
            try {
                VisibilityRegistrationContent = !BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth;
                AvatarUrl = BaseSingleton<GlobalSetting>.Instance.UserProfile.AvatarUrl;
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR:{ex.Message}");
                Debugger.Break();
            }
        }
    }
}
