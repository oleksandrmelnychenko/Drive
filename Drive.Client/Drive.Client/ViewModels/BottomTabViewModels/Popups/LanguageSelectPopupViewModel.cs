using Drive.Client.DataItems.ProfileSettings;
using Drive.Client.Helpers.Localize;
using Drive.Client.Models.DataItems.ProfileSettings;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews.Popups;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using Drive.Client.Helpers;

namespace Drive.Client.ViewModels.BottomTabViewModels.Popups {
    public class LanguageSelectPopupViewModel : PopupBaseViewModel {

        private readonly IProfileSettingsDataItems _profileSettingsDataItems;

        public LanguageSelectPopupViewModel(
            IProfileSettingsDataItems profileSettingsDataItems) {

            _profileSettingsDataItems = profileSettingsDataItems;

            Languages = _profileSettingsDataItems.BuildLanguageDataItems();
            SelectedLanguage = Languages.FirstOrDefault<LanguageDataItem>(languageItem => languageItem.Language == BaseSingleton<GlobalSetting>.Instance.AppInterfaceConfigurations.LanguageInterface);
        }

        public ICommand SelectLanguageCommand => new Command((object param) => {
            if (param is LanguageDataItem selectedLanguageDataItem) {
                SelectedLanguage = selectedLanguageDataItem;

                BaseSingleton<GlobalSetting>.Instance.AppInterfaceConfigurations.LanguageInterface = selectedLanguageDataItem.Language;
                BaseSingleton<GlobalSetting>.Instance.AppInterfaceConfigurations.SaveChanges();

                ResourceLoader.Instance.CultureInfo = selectedLanguageDataItem.Culture;

                ClosePopupCommand.Execute(null);
            }
        });

        public override Type RelativeViewType => typeof(LanguageSelectPopupView);

        List<LanguageDataItem> _languages;
        public List<LanguageDataItem> Languages {
            get => _languages;
            private set => SetProperty<List<LanguageDataItem>>(ref _languages, value);
        }

        LanguageDataItem _selectedLanguage;
        public LanguageDataItem SelectedLanguage {
            get => _selectedLanguage;
            private set => SetProperty<LanguageDataItem>(ref _selectedLanguage, value);
        }
    }
}
