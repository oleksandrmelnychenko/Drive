using Drive.Client.DataItems.ProfileSettings;
using Drive.Client.Models.DataItems.ProfileSettings;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews.Popups;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels.Popups {
    public class LanguageSelectPopupViewModel : PopupBaseViewModel {

        private readonly IProfileSettingsDataItems _profileSettingsDataItems;

        public LanguageSelectPopupViewModel(
            IProfileSettingsDataItems profileSettingsDataItems) {

            _profileSettingsDataItems = profileSettingsDataItems;

            Languages = _profileSettingsDataItems.BuildLanguageDataItems();
        }

        public ICommand SelectLanguageCommand => new Command(async (object param) => {
            if (param is LanguageDataItem selectedLanguageDataItem) {
                await DialogService.ToastAsync(string.Format("Select language command in developing. {0}", selectedLanguageDataItem.Title));
            }
        });

        public override Type RelativeViewType => typeof(LanguageSelectPopupView);

        List<LanguageDataItem> _languages;
        public List<LanguageDataItem> Languages {
            get => _languages;
            private set => SetProperty<List<LanguageDataItem>>(ref _languages, value);
        }
    }
}
