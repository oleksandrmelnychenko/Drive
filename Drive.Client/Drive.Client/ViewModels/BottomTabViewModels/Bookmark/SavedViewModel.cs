using Drive.Client.Helpers;
using Drive.Client.Helpers.Localize;
using Drive.Client.Resources.Resx;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.IdentityAccounting.Registration;
using Drive.Client.Views.BottomTabViews.Bookmark;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels.Bookmark
{
    public sealed class SavedViewModel : NestedViewModel, IVisualFiguring {

        public Type RelativeViewType => typeof(SavedView);

        bool _visibilityClosedView;
        public bool VisibilityClosedView {
            get { return _visibilityClosedView; }
            set { SetProperty(ref _visibilityClosedView, value); }
        }

        StringResource _tabHeader = ResourceLoader.Instance.GetString(nameof(AppStrings.SavedUpperCase));
        public StringResource TabHeader {
            get => _tabHeader;
            private set => SetProperty(ref _tabHeader, value);
        }

        public ICommand SignInCommand => new Command(async () => await NavigationService.NavigateToAsync<SignInPhoneNumberStepViewModel>());

        public ICommand SignUpCommand => new Command(async () => await NavigationService.NavigateToAsync<PhoneNumberRegisterStepViewModel>());

        /// <summary>
        ///     ctor().
        /// </summary>
        public SavedViewModel() {

        }

        public override void Dispose() {
            base.Dispose();

           
        }

        public override Task InitializeAsync(object navigationData) {
            UpdateView();

            return base.InitializeAsync(navigationData);
        }

        private void UpdateView() {
            VisibilityClosedView = !BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth;
        }
    }
}
