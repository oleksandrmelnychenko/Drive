using Drive.Client.ViewModels.Base;
using Drive.Client.Views.Popups;
using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.ViewModels.Popups {
    public class UpdateAppVersionPopupViewModel : PopupBaseViewModel {

        /// <summary>
        ///     ctor().
        /// </summary>
        public UpdateAppVersionPopupViewModel() {

        }

        public override Type RelativeViewType => typeof(UpdateAppVersionPopupView);
    }
}
