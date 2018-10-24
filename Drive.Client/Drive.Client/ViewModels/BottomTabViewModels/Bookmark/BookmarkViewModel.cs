using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Drive.Client.Helpers;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.Services.Vehicle;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews;
using Drive.Client.Views.BottomTabViews.Bookmark;

namespace Drive.Client.ViewModels.BottomTabViewModels.Bookmark {
    public sealed class BookmarkViewModel : TabbedViewModelBase {

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
        public BookmarkViewModel() {
            SearchTabs = new List<IVisualFiguring>() {
                DependencyLocator.Resolve<UserVehiclesViewModel>(),
                DependencyLocator.Resolve<SavedViewModel>()
            };
            SearchTabs.ForEach(searchTab => searchTab.InitializeAsync(this));
        }

        protected override void TabbViewModelInit() {
            RelativeViewType = typeof(BookmarkView);
            TabIcon = IconPath.BOOKMARK;
            HasBackgroundItem = false;
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
    }
}
