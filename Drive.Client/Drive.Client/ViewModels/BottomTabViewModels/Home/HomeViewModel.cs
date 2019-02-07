using Drive.Client.Helpers;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews.Home;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.BottomTabViewModels.Home {
    public sealed class HomeViewModel : TabbedViewModelBase {

        List<IVisualFiguring> _postTabs;
        public List<IVisualFiguring> PostTabs {
            get => _postTabs;
            private set {
                _postTabs?.ForEach(searchTab => searchTab.Dispose());
                SetProperty(ref _postTabs, value);
            }
        }

        int _selectedTabIndex;
        public int SelectedTabIndex {
            get => _selectedTabIndex;
            set => SetProperty(ref _selectedTabIndex, value);
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public HomeViewModel() {
            PostTabs = new List<IVisualFiguring>() {
                DependencyLocator.Resolve<BandViewModel>(),
                DependencyLocator.Resolve<SavedViewModel>()
            };
            PostTabs.ForEach(searchTab => searchTab.InitializeAsync(this));
        }

        protected override void TabbViewModelInit() {
            RelativeViewType = typeof(HomeView);
            TabIcon = IconPath.HOME;
            HasBackgroundItem = false;
        }

        public override Task InitializeAsync(object navigationData) {
            if (navigationData is SelectedBottomBarTabArgs) {

            } 

            PostTabs?.ForEach(searchTab => searchTab.InitializeAsync(navigationData));

            return base.InitializeAsync(navigationData);
        }

        public override void Dispose() {
            base.Dispose();

            PostTabs?.ForEach(searchTab => searchTab.Dispose());
        }
    }
}
