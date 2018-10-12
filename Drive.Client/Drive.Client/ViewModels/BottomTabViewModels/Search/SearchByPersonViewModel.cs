using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews.Search;
using System;

namespace Drive.Client.ViewModels.BottomTabViewModels.Search {
    public class SearchByPersonViewModel : NestedViewModel, IVisualFiguring {

        public Type RelativeViewType => typeof(SearchByPersonView);

        private string _tabHeader = "ФІЗ. ОСОБА";
        public string TabHeader {
            get => _tabHeader;
            private set => SetProperty<string>(ref _tabHeader, value);
        }
    }
}
