using Drive.Client.Models.Calculator;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.Popups;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.Popups {
    public sealed class CustomsResultPopupViewModel : PopupBaseViewModel {

        CustomsResult _customsResult;
        public CustomsResult CustomsResult {
            get { return _customsResult; }
            set { SetProperty(ref _customsResult, value); }
        }

        public override Type RelativeViewType => typeof(CustomsResultPopupView);

        public ICommand CancelCommand => new Command(() => ClosePopupCommand.Execute(null));

        /// <summary>
        ///     ctor().
        /// </summary>
        public CustomsResultPopupViewModel() {

        }

        protected override void OnShowPopupCommand(object param) {
            base.OnShowPopupCommand(param);

            if (param is CustomsResult customsResult) {
                CustomsResult = customsResult;
            }
        }

        protected override void OnClosePopupCommand(object param) {
            base.OnClosePopupCommand(param);

            CustomsResult = null;
        }

        protected override void OnIsPopupVisible() {
            base.OnIsPopupVisible();
            if (!IsPopupVisible) {
                CustomsResult = null;
            }
        }
    }
}
