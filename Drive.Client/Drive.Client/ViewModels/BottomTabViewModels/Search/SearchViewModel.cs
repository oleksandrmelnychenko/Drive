using Drive.Client.Helpers;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.BottomTabViewModels.Search {
    public sealed class SearchViewModel : TabbedViewModelBase {

        public SearchViewModel() {
            SearchTabs = new List<IVisualFiguring>() {
                DependencyLocator.Resolve<SearchByCarIdViewModel>(),
                DependencyLocator.Resolve<SearchByPersonViewModel>()
            };
            SearchTabs.ForEach(searchTab => searchTab.InitializeAsync(this));
        }

        private List<IVisualFiguring> _searchTabs;
        public List<IVisualFiguring> SearchTabs {
            get => _searchTabs;
            private set {
                value?.ForEach(searchTab => searchTab.Dispose());
                SetProperty<List<IVisualFiguring>>(ref _searchTabs, value);
            }
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
    }
}
