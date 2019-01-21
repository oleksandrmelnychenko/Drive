using Drive.Client.Helpers;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.BottomTabViewModels.Popups;
using Drive.Client.ViewModels.BottomTabViewModels.Search;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels {
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
            if (IsSearchByPhotoAvailable) {
                await DialogService.ToastAsync("TODO: `search by image` flow starts here");
            }
            else {
                if (!string.IsNullOrEmpty(BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken)) {
                    PostTypePopupViewModel.ShowPopupCommand.Execute(null);
                }
                else {
                    await DialogService.ToastAsync("In developing. TODO: resolve behaviour when user is not authorized.");
                }
            }
        });

        private bool _isSearchByPhotoAvailable;
        public bool IsSearchByPhotoAvailable {
            get => _isSearchByPhotoAvailable;
            private set => SetProperty<bool>(ref _isSearchByPhotoAvailable, value);
        }

        public override void Dispose() {
            base.Dispose();

            PostTypePopupViewModel?.Dispose();
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is SelectedBottomBarTabArgs) { }
            else if (navigationData is SomeBottomTabWasSelectedArgs someBottomTabWasSelectedArgs) {
                IsSearchByPhotoAvailable = someBottomTabWasSelectedArgs.SelectedTabType == typeof(SearchViewModel);
            }

            PostTypePopupViewModel?.InitializeAsync(navigationData);

            return base.InitializeAsync(navigationData);
        }

        protected override void TabbViewModelInit() {
            TabIcon = IconPath.POSTBUILDER;
            //RelativeViewType = null;
            HasBackgroundItem = true;
        }
    }
}
