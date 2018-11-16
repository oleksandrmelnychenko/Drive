using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews.Popups;
using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.ViewModels.BottomTabViewModels.Popups {
    public class PostTypePopupViewModel : PopupBaseViewModel {

        public override Type RelativeViewType => typeof(PostTypePopupView);

        /// <summary>
        ///     ctor().
        /// </summary>
        public PostTypePopupViewModel() {
                
        }
    }
}
