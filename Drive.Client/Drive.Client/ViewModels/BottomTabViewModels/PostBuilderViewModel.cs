using Drive.Client.Helpers;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.Services.Identity;
using Drive.Client.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels {
    public sealed class PostBuilderViewModel : TabbedViewModelBase, IActionBottomBarTab {

        private readonly IIdentityService _identityService;

        /// <summary>
        ///     ctor().
        /// </summary>
        public PostBuilderViewModel(IIdentityService identityService) {
            _identityService = identityService;
        }

        public ICommand TabActionCommand => new Command(()=> {
            ///
            /// TODO: show your popup etc...
            /// 
        });

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is SelectedBottomBarTabArgs) {

            }

            return base.InitializeAsync(navigationData);
        }

        protected override void TabbViewModelInit() {
            TabIcon = IconPath.POSTBUILDER;
            RelativeViewType = null;
            HasBackgroundItem = true;
        }
    }
}
