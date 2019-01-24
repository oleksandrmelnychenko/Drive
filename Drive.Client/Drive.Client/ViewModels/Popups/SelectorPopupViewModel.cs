using Drive.Client.ViewModels.Base;
using Drive.Client.Views.Popups;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.Popups {
    public class SelectorPopupViewModel : PopupBaseViewModel {

        public event EventHandler<IPopupSelectionItem> ItemSelected = delegate { };

        public ICommand SelectItemCommand => new Command((object param) => {
            if (param is IPopupSelectionItem popupSelectionItem) {
                ItemSelected(this, popupSelectionItem);
            }

            ClosePopupCommand.Execute(null);
        });

        public override Type RelativeViewType => typeof(SelectorPopupView);

        private IEnumerable<IPopupSelectionItem> _itemsToSelect;
        public IEnumerable<IPopupSelectionItem> ItemsToSelect {
            get => _itemsToSelect;
            private set => SetProperty<IEnumerable<IPopupSelectionItem>>(ref _itemsToSelect, value);
        }

        protected async override void OnShowPopupCommand(object param) {
            base.OnShowPopupCommand(param);

            if (param is IEnumerable<IPopupSelectionItem> selectionItems) {
                ItemsToSelect = selectionItems;
            }
            else {
                Debugger.Break();
                await DialogService.ToastAsync("Unsuported command parameter type");
            }
        }

        protected override void OnClosePopupCommand(object param) {
            base.OnClosePopupCommand(param);

            ItemsToSelect = null;
            Title = null;
        }
    }
}
