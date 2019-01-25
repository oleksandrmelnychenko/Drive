using Drive.Client.Helpers.Localize;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.Resources.Resx;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews.Profile;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.BottomTabViewModels.Profile {
    public sealed class MyPostsViewModel : NestedViewModel, IVisualFiguring, ISwitchTab {

        StringResource _tabHeader;
        public StringResource TabHeader {
            get => _tabHeader;
            private set => SetProperty(ref _tabHeader, value);
        }

        public Type RelativeViewType => typeof(MyPostsView);

        /// <summary>
        ///     ctor().
        /// </summary>
        public MyPostsViewModel() {

        }

        protected override void ResolveStringResources() {
            base.ResolveStringResources();

            TabHeader = ResourceLoader.GetString(nameof(AppStrings.YourPostsUpperCase));
        }

        public override Task InitializeAsync(object navigationData) {
            if (navigationData is SelectedBottomBarTabArgs) {
                
            }

            return base.InitializeAsync(navigationData);
        }

        public void ClearAfterTabTap() {
            
        }

        public void TabClicked() {
            
        }
    }
}
