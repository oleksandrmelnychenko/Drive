using Drive.Client.Controls.Popups;
using Drive.Client.Helpers.Localize;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.Base {
    public abstract class PopupBaseViewModel : NestedViewModel, IPopupContext {

        private StringResource _title;
        public StringResource Title {
            get => _title;
            set => SetProperty<StringResource>(ref _title, value);
        }

        bool _isPopupVisible;
        public bool IsPopupVisible {
            get => _isPopupVisible;
            set {
                SetProperty(ref _isPopupVisible, value);

                OnIsPopupVisible();
            }
        }

        public ICommand ShowPopupCommand => new Command((object param) => {
            OnShowPopupCommand(param);
            UpdatePopupScopeVisibility(true);
            IsPopupVisible = true;
        });

        public ICommand ClosePopupCommand => new Command((object param) => {
            OnClosePopupCommand(param);
            UpdatePopupScopeVisibility(false);
        });

        public abstract Type RelativeViewType { get; }

        public override Task InitializeAsync(object navigationData) {
            if (navigationData is ContentPageBaseViewModel pageBaseViewModel) {
                if (!pageBaseViewModel.Popups.Contains(this)) {
                    pageBaseViewModel.Popups.Add(this);
                }
            }

            return base.InitializeAsync(navigationData);
        }

        protected virtual void OnIsPopupVisible() { }

        protected virtual void OnShowPopupCommand(object param) { }

        protected virtual void OnClosePopupCommand(object param) { }
    }
}
