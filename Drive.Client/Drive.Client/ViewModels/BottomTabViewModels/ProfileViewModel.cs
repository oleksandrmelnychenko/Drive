using Drive.Client.Helpers;
using Drive.Client.Services.Identity.IdentityUtility;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.BottomTabViewModels.Popups;
using Drive.Client.ViewModels.IdentityAccounting.EditProfile;
using Drive.Client.ViewModels.IdentityAccounting.Registration;
using Drive.Client.Views.BottomTabViews;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels {
    public sealed class ProfileViewModel : NestedViewModel, IBottomBarTab {

        private readonly IIdentityUtilityService _identityUtilityService;

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

        public ICommand FacebookLoginCommand => new Command(async () => await DialogService.ToastAsync("Facebook login command in developing"));

        public ICommand LoginCommand => new Command(async () => await NavigationService.NavigateToAsync<SignInPhoneNumberStepViewModel>());

        public ICommand RegisterCommand => new Command(async () => await NavigationService.NavigateToAsync<PhoneNumberRegisterStepViewModel>());

        public ICommand EditPhoneNumberCommand => new Command(async () => await NavigationService.NavigateToAsync<EditPhoneNumberViewModel>());

        public ICommand EditUserNameCommand => new Command(async () => await NavigationService.NavigateToAsync<EditUserNameViewModel>());

        public ICommand EditEmailCommand => new Command(async () => await NavigationService.NavigateToAsync<EditEmailViewModel>());

        public ICommand LogOutCommand => new Command(async () => await _identityUtilityService.LogOutAsync());

        public ICommand ChangeAvatarCommand => new Command(async () => await DialogService.ToastAsync("Change avatar command in developing"));

        /// <summary>
        ///     ctor().
        /// </summary>
        public ProfileViewModel(IIdentityUtilityService identityUtilityService) {
            _identityUtilityService = identityUtilityService;

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
        }

        private void UpdateView() {
            try {
                VisibilityRegistrationContent = !BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth;
                UserName = BaseSingleton<GlobalSetting>.Instance.UserProfile?.UserName;
                PhoneNumber = BaseSingleton<GlobalSetting>.Instance.UserProfile?.PhoneNumber;
                Email = BaseSingleton<GlobalSetting>.Instance.UserProfile?.Email;
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR:{ex.Message}");
                Debugger.Break();
            }
        }
    }
}
