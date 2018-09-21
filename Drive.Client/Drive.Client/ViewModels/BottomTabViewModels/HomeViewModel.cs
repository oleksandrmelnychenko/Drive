using Drive.Client.Helpers;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views;
using Drive.Client.Views.BottomTabViews;
using System;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.BottomTabViewModels {
    public sealed class HomeViewModel : NestedViewModel, IBottomBarTab {

        public bool IsBudgeVisible { get; private set; }

        public int BudgeCount { get; private set; }

        public string TabHeader { get; private set; }

        public string TabIcon { get; private set; } = IconPath.HOME;

        public Type RelativeViewType { get; private set; } = typeof(HomeView);

        public bool HasBackgroundItem => false;

        /// <summary>
        ///     ctor().
        /// </summary>
        public HomeViewModel() {

        }

        public override Task InitializeAsync(object navigationData) {

            return base.InitializeAsync(navigationData);
        }
    }
}
