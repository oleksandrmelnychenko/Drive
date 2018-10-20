using Drive.Client.Helpers;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews.Search;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.BottomTabViewModels.Search {
    public sealed class SearchViewModel : TabbedViewModelBase, IClearedAfterTabTap {

        List<IVisualFiguring> _searchTabs;
        public List<IVisualFiguring> SearchTabs {
            get => _searchTabs;
            private set {
                _searchTabs?.ForEach(searchTab => searchTab.Dispose());
                SetProperty(ref _searchTabs, value);
            }
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public SearchViewModel() {
            SearchTabs = new List<IVisualFiguring>() {
                DependencyLocator.Resolve<SearchByCarIdViewModel>(),
                DependencyLocator.Resolve<SearchByPersonViewModel>()
            };
            SearchTabs.ForEach(searchTab => searchTab.InitializeAsync(this));
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

            SearchTabs?.ForEach(searchTab => searchTab.InitializeAsync(navigationData));

            return base.InitializeAsync(navigationData);
        }

        public void ClearAfterTabTap() {
            try {
                SearchTabs?.ForEach(searchTab => (searchTab as IClearedAfterTabTap)?.ClearAfterTabTap());
            }
            catch (Exception ex) {
                Debugger.Break();
            }
        }
    }
}
