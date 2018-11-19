using Drive.Client.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.ActionBars {
    internal sealed class NewPostActionBarViewModel : CommonActionBarViewModel {

        public ICommand PublishCommand => new Command(() => NavigationService.LastPageViewModel.InitializeAsync(null));

        /// <summary>
        ///     ctor().
        /// </summary>
        public NewPostActionBarViewModel() {
            
        }
    }
}
