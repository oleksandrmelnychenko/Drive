using Drive.Client.DataItems.ProfileSettings;
using Drive.Client.Helpers;
using Drive.Client.Models.DataItems.ProfileSettings;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels.Popups {
    public class LanguageSelectPopupViewModel : PopupBaseViewModel {

        private readonly IProfileSettingsDataItems _profileSettingsDataItems;

        public LanguageSelectPopupViewModel(IProfileSettingsDataItems profileSettingsDataItems) {
            _profileSettingsDataItems = profileSettingsDataItems;

            Languages = _profileSettingsDataItems.BuildLanguageDataItems();
            SelectedLanguage = Languages.FirstOrDefault(languageItem => languageItem.Language.LanguageInterface == BaseSingleton<GlobalSetting>.Instance.AppInterfaceConfigurations.LanguageInterface.LanguageInterface);
        }

        public ICommand SelectLanguageCommand => new Command((object param) => {
            if (param is LanguageDataItem selectedLanguageDataItem) {
                SelectedLanguage = selectedLanguageDataItem;

                BaseSingleton<GlobalSetting>.Instance.AppInterfaceConfigurations.LanguageInterface = selectedLanguageDataItem.Language;
                BaseSingleton<GlobalSetting>.Instance.AppInterfaceConfigurations.SaveChanges();

                //ResourceLoader.Instance.CultureInfo = selectedLanguageDataItem.Language.Culture;

                // Raise selected language.
                BaseSingleton<GlobalSetting>.Instance.AppMessagingEvents.LanguageEvents.OnLanguageChanged(selectedLanguageDataItem.Language);

                ClosePopupCommand.Execute(null);
            }
        });

        public override Type RelativeViewType => typeof(LanguageSelectPopupView);

        List<LanguageDataItem> _languages;
        public List<LanguageDataItem> Languages {
            get => _languages;
            private set => SetProperty(ref _languages, value);
        }

        LanguageDataItem _selectedLanguage;
        public LanguageDataItem SelectedLanguage {
            get => _selectedLanguage;
            private set => SetProperty(ref _selectedLanguage, value);
        }
    }
}
