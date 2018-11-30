using Drive.Client.Helpers;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.Services.Identity;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.BottomTabViewModels.Popups;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels {
    public sealed class PostBuilderViewModel : TabbedViewModelBase, IActionBottomBarTab {

        PostTypePopupViewModel _postTypePopupViewModel;
        public PostTypePopupViewModel PostTypePopupViewModel {
            get => _postTypePopupViewModel;
            private set {
                _postTypePopupViewModel?.Dispose();
                SetProperty(ref _postTypePopupViewModel, value);
            }
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public PostBuilderViewModel() {
            PostTypePopupViewModel = DependencyLocator.Resolve<PostTypePopupViewModel>();
            PostTypePopupViewModel.InitializeAsync(this);
        }

        public ICommand TabActionCommand => new Command(()=> {
            //PostTypePopupViewModel.ShowPopupCommand.Execute(null);
        });

        public override void Dispose() {
            base.Dispose();

            PostTypePopupViewModel?.Dispose();
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is SelectedBottomBarTabArgs) {

            }

            PostTypePopupViewModel?.InitializeAsync(navigationData);

            return base.InitializeAsync(navigationData);
        }

        protected override void TabbViewModelInit() {
            TabIcon = IconPath.POSTBUILDER;
            RelativeViewType = null;
            HasBackgroundItem = true;
        }
    }
}
