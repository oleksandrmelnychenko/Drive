using Drive.Client.Helpers;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews;

namespace Drive.Client.ViewModels.BottomTabViewModels {
    public sealed class HomeViewModel : TabbedViewModelBase {

        protected override void TabbViewModelInit() {
            RelativeViewType = typeof(HomeView);
            TabIcon = IconPath.HOME;
            HasBackgroundItem = false;
        }
    }
}
