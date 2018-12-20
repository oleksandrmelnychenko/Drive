using Drive.Client.Helpers;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews.Search;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.BottomTabViewModels.Search {
    public sealed class SearchViewModel : TabbedViewModelBase, ISwitchTab {

        List<IVisualFiguring> _searchTabs;
        public List<IVisualFiguring> SearchTabs {
            get => _searchTabs;
            private set {
                _searchTabs?.ForEach(searchTab => searchTab.Dispose());
                SetProperty(ref _searchTabs, value);
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
        public SearchViewModel() {
            SearchTabs = new List<IVisualFiguring>() {
                //DependencyLocator.Resolve<SearchByPolandCarIdViewModel>(),
                DependencyLocator.Resolve<SearchByCarIdViewModel>(),
                DependencyLocator.Resolve<SearchByPersonViewModel>()
            };
            SearchTabs.ForEach(searchTab => searchTab.InitializeAsync(this));

            SelectedTabIndex = 0;
        }

        protected override void TabbViewModelInit() {
            RelativeViewType = typeof(SearchView);
            TabIcon = IconPath.SEARCH;
        }

        public override void Dispose() {
            base.Dispose();

            SearchTabs?.ForEach(searchTab => searchTab.Dispose());
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is SelectedBottomBarTabArgs) {

            }

            SearchTabs?.ForEach(searchTab => searchTab.InitializeAsync(navigationData));

            return base.InitializeAsync(navigationData);
        }

        public void ClearAfterTabTap() {
            try {
                SearchTabs?.ForEach(searchTab => (searchTab as ISwitchTab)?.ClearAfterTabTap());
            }
            catch (Exception ex) {
                Debugger.Break();
            }
        }

        public void TabClicked() { }
    }
}
