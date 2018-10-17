using Drive.Client.Helpers;
using Drive.Client.Helpers.Localize;
using Drive.Client.Models.EntityModels.Search;
using Drive.Client.ViewModels.Base;
using System;
using Xamarin.Forms;

namespace Drive.Client.Models.DataItems.Vehicle {
    public class ResidentRequestDataItem : NestedViewModel {

        public ResidentRequest ResidentRequest { get; set; }

        Color _statusColor;
        public Color StatusColor {
            get { return _statusColor; }
            set { SetProperty(ref _statusColor, value); }
        }

        DateTime _created;
        public DateTime Created {
            get { return _created; }
            set { SetProperty(ref _created, value); }
        }

        StringResource _countOutput;
        public StringResource CountOutput {
            get { return _countOutput; }
            set { SetProperty(ref _countOutput, value); }
        }

        StringResource _status;
        public StringResource Status {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        private void LanguageEvents_LanguageChanged(object sender, Helpers.AppEvents.Events.Args.LanguageChangedArgs e) {
            OnPropertyChanged(nameof(Created));
        }

        protected override void OnSubscribeOnAppEvents() {
            base.OnSubscribeOnAppEvents();

            BaseSingleton<GlobalSetting>.Instance.AppMessagingEvents.LanguageEvents.LanguageChanged += LanguageEvents_LanguageChanged;
        }

        protected override void OnUnsubscribeFromAppEvents() {
            base.OnUnsubscribeFromAppEvents();

            BaseSingleton<GlobalSetting>.Instance.AppMessagingEvents.LanguageEvents.LanguageChanged -= LanguageEvents_LanguageChanged;
        }
    }
}
