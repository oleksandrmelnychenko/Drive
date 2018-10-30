using Drive.Client.Helpers;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels {
    public sealed class HomeViewModel : TabbedViewModelBase {

      
        public ICommand TODOCommand => new Command(() => {
            
        });

        DateTime? _TODO;
        public DateTime? TODO {
            get => _TODO;
            set => SetProperty(ref _TODO, value);
        }
       
        /// <summary>
        ///     ctor().
        /// </summary>
        public HomeViewModel() {

        }

        protected override void TabbViewModelInit() {
            RelativeViewType = typeof(HomeView);
            TabIcon = IconPath.HOME;
            HasBackgroundItem = false;
        }


        protected override void OnSubscribeOnAppEvents() {
            base.OnSubscribeOnAppEvents();

            
        }

        protected override void OnUnsubscribeFromAppEvents() {
            base.OnUnsubscribeFromAppEvents();

            
        }

       
        public override Task InitializeAsync(object navigationData) {
            if (navigationData is SelectedBottomBarTabArgs) {

            }

            return base.InitializeAsync(navigationData);
        }
    }
}
