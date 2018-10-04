using Drive.Client.Helpers;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.IdentityAccounting.Registration;
using Drive.Client.Views.BottomTabViews;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels {
    public sealed class ProfileViewModel : NestedViewModel, IBottomBarTab {

        public bool IsBudgeVisible { get; private set; }

        public int BudgeCount { get; private set; }

        public string TabHeader { get; private set; }

        public string TabIcon { get; private set; } = IconPath.PROFILE;

        public Type RelativeViewType { get; private set; } = typeof(ProfileView);

        public bool HasBackgroundItem => false;

        bool _visibilityRegistrationContent;
        public bool VisibilityRegistrationContent {
            get { return _visibilityRegistrationContent; }
            set { SetProperty(ref _visibilityRegistrationContent, value); }
        }

        public ICommand FacebookLoginCommand => new Command(async () => await DialogService.ToastAsync("Facebook login command in developing"));

        public ICommand LoginCommand => new Command(async () => await DialogService.ToastAsync("Login command in developing"));

        public ICommand RegisterCommand => new Command(async () => await NavigationService.NavigateToAsync<PhoneNumberRegisterStepViewModel>());

        public ICommand AcceptCommand => new Command(async () => await DialogService.ToastAsync(" Accept command in developing"));

        /// <summary>
        ///     ctor().
        /// </summary>
        public ProfileViewModel() {
            VisibilityRegistrationContent = !BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth;
        }

        public override Task InitializeAsync(object navigationData) {
            UpdateView();

            return base.InitializeAsync(navigationData);
        }

        private void UpdateView() {
            VisibilityRegistrationContent = !BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth;
        }
    }
}
