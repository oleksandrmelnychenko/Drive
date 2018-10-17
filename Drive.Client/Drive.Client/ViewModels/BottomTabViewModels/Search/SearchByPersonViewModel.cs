using Drive.Client.Helpers.Localize;
using Drive.Client.Resources.Resx;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews.Search;
using System;

namespace Drive.Client.ViewModels.BottomTabViewModels.Search {
    public class SearchByPersonViewModel : NestedViewModel, IVisualFiguring {

        public Type RelativeViewType => typeof(SearchByPersonView);

        StringResource _tabHeader = ResourceLoader.Instance.GetString(nameof(AppStrings.PersonUpperCase));
        public StringResource TabHeader {
            get => _tabHeader;
            private set => SetProperty(ref _tabHeader, value);
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public SearchByPersonViewModel() {

        }
    }
}
