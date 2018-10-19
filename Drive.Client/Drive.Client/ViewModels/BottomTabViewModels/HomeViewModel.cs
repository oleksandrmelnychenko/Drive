using Drive.Client.Helpers;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews;
using System;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels {
    public sealed class HomeViewModel : TabbedViewModelBase {

        protected override void TabbViewModelInit() {
            RelativeViewType = typeof(HomeView);
            TabIcon = IconPath.HOME;
            HasBackgroundItem = false;

            //TODO = DateTime.Now;
        }

        public ICommand TODOCommand => new Command(() => {
            DateTime? selectedValue = _TODO;
        });

        DateTime? _TODO;
        public DateTime? TODO {
            get => _TODO;
            set => SetProperty<DateTime?>(ref _TODO, value);
        }

        CultureInfo _datePickerCulture = new CultureInfo(BaseSingleton<GlobalSetting>.Instance.AppInterfaceConfigurations.LanguageInterface.LocaleId);
        public CultureInfo DatePickerCulture {
            get => _datePickerCulture;
            private set => SetProperty<CultureInfo>(ref _datePickerCulture, value);
        }

        protected override void OnSubscribeOnAppEvents() {
            base.OnSubscribeOnAppEvents();

            BaseSingleton<GlobalSetting>.Instance.AppMessagingEvents.LanguageEvents.LanguageChanged += OnLanguageEventsLanguageChanged;
        }

        protected override void OnUnsubscribeFromAppEvents() {
            base.OnUnsubscribeFromAppEvents();

            BaseSingleton<GlobalSetting>.Instance.AppMessagingEvents.LanguageEvents.LanguageChanged -= OnLanguageEventsLanguageChanged;
        }

        private void OnLanguageEventsLanguageChanged(object sender, Helpers.AppEvents.Events.Args.LanguageChangedArgs e) => DatePickerCulture = new CultureInfo(e.LanguageId);
    }
}
