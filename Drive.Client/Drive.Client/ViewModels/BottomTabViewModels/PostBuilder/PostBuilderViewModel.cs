using Drive.Client.Helpers;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.Models.Identities;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.BottomTabViewModels.Popups;
using Drive.Client.ViewModels.BottomTabViewModels.Search;
using Drive.Client.Views.BottomTabViews.PostBuilder;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels.PostBuilder {
    public sealed class PostBuilderViewModel : ViewLessTabViewModel, IActionBottomBarTab {

        PostTypePopupViewModel _postTypePopupViewModel;
        public PostTypePopupViewModel PostTypePopupViewModel {
            get => _postTypePopupViewModel;
            private set {
                _postTypePopupViewModel?.Dispose();
                SetProperty(ref _postTypePopupViewModel, value);
            }
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public PostBuilderViewModel() {
            PostTypePopupViewModel = DependencyLocator.Resolve<PostTypePopupViewModel>();
            PostTypePopupViewModel.InitializeAsync(this);
        }

        public ICommand TabActionCommand => new Command(async () => {
            switch (UserButtonState) {
                case UserButtonState.CreateNewPost:
                    if (!string.IsNullOrEmpty(BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken)) {
                        PostTypePopupViewModel.ShowPopupCommand.Execute(null);
                    }
                    else {
                        await DialogService.ToastAsync("In developing. TODO: resolve behaviour when user is not authorized.");
                    }
                    break;
                case UserButtonState.SearchByImage:
                    await DialogService.ToastAsync("TODO: `search by image` flow starts here");
                    break;
                default:
                    Debugger.Break();
                    throw new InvalidOperationException("Unresolved UserButtonState");
            }
        });

        private UserButtonState _userButtonState;
        public UserButtonState UserButtonState {
            get => _userButtonState;
            private set => SetProperty<UserButtonState>(ref _userButtonState, value);
        }

        public override void Dispose() {
            base.Dispose();

            PostTypePopupViewModel?.Dispose();
        }

        public override Task InitializeAsync(object navigationData) {
            if (navigationData is SelectedBottomBarTabArgs) { }
            else if (navigationData is SomeBottomTabWasSelectedArgs someBottomTabWasSelectedArgs) {
                UserButtonState = someBottomTabWasSelectedArgs.SelectedTabType == typeof(SearchViewModel) ? UserButtonState.SearchByImage : UserButtonState.CreateNewPost;
            }

            PostTypePopupViewModel?.InitializeAsync(navigationData);

            return base.InitializeAsync(navigationData);
        }

        protected override void TabbViewModelInit() {
            TabIcon = IconPath.POSTBUILDER;
            //RelativeViewType = null;
            HasBackgroundItem = true;
            BottomTasselViewType = typeof(UserButtonBottomItemView);
        }
    }
}
