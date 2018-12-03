using Drive.Client.Helpers;
using Drive.Client.Helpers.AppEvents.Events.Args;
using Drive.Client.Helpers.Localize;
using Drive.Client.Models.EntityModels.Search;
using Xamarin.Forms;

namespace Drive.Client.Models.DataItems.Vehicle {
    public class ResidentRequestDataItem : BaseRequestDataItem {

        public ResidentRequest ResidentRequest { get; set; }

        Color _statusColor;
        public Color StatusColor {
            get { return _statusColor; }
            set { SetProperty(ref _statusColor, value); }
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

        private void LanguageEvents_LanguageChanged(object sender, LanguageChangedArgs e) {
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
