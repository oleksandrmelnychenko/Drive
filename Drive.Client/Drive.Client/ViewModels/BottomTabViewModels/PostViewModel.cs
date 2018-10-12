using Drive.Client.Helpers;
using Drive.Client.Services.Identity;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.BottomTabViewModels {
    public sealed class PostViewModel : TabbedViewModelBase {

        private readonly IIdentityService _identityService;

        /// <summary>
        ///     ctor().
        /// </summary>
        public PostViewModel(IIdentityService identityService) {
            _identityService = identityService;
        }

        public override Task InitializeAsync(object navigationData) {

            return base.InitializeAsync(navigationData);
        }

        protected override void TabbViewModelInit() {
            TabIcon = IconPath.POST;
            RelativeViewType = typeof(PostView);
            HasBackgroundItem = true;
        }
    }
}
