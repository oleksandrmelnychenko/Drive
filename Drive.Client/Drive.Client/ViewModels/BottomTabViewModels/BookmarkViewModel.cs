using Drive.Client.Helpers;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews;

namespace Drive.Client.ViewModels.BottomTabViewModels {
    public sealed class BookmarkViewModel : TabbedViewModelBase {

        protected override void TabbViewModelInit() {
            RelativeViewType = typeof(BookmarkView);
            TabIcon = IconPath.BOOKMARK;
            HasBackgroundItem = false;
        }
    }
}
