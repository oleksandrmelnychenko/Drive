using Drive.Client.Helpers;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.Services.Identity;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.BottomTabViewModels {
    public sealed class PostBuilderViewModel : TabbedViewModelBase {

        private readonly IIdentityService _identityService;

        /// <summary>
        ///     ctor().
        /// </summary>
        public PostBuilderViewModel(IIdentityService identityService) {
            _identityService = identityService;
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is SelectedBottomBarTabArgs) {

            }

            return base.InitializeAsync(navigationData);
        }

        protected override void TabbViewModelInit() {
            TabIcon = IconPath.POSTBUILDER;
            RelativeViewType = typeof(PostBuilderView);
            HasBackgroundItem = true;
        }
    }
}
