using Drive.Client.Helpers.Localize;
using Drive.Client.Resources.Resx;
using Drive.Client.ViewModels.Base;

namespace Drive.Client.ViewModels.Popups {
    public abstract class RequestInfoPopupBaseViewModel : PopupBaseViewModel {

        public static StringResource COMMON_REQUEST_INFO_MAIN_TITLE;
        public static StringResource COMMON_REQUEST_INFO_PLAIN_TEXT;
        public static StringResource NO_RESULTS_OUTPUT;
        public static StringResource VEHICLE_FOUND_OUTPUT;

        StringResource _mainTitle;
        public StringResource MainTitle {
            get => _mainTitle;
            protected set => SetProperty(ref _mainTitle, value);
        }

        StringResource _plainOutputText;
        public StringResource PlainOutputText {
            get => _plainOutputText;
            protected set => SetProperty(ref _plainOutputText, value);
        }

        protected override void ResolveStringResources() {
            base.ResolveStringResources();

            COMMON_REQUEST_INFO_MAIN_TITLE = ResourceLoader.GetString(nameof(AppStrings.RequestSent));
            COMMON_REQUEST_INFO_PLAIN_TEXT = ResourceLoader.GetString(nameof(AppStrings.AnswerReady));
            NO_RESULTS_OUTPUT = ResourceLoader.GetString(nameof(AppStrings.VehicleRequestGiveNoResults));
            VEHICLE_FOUND_OUTPUT = ResourceLoader.GetString(nameof(AppStrings.VehicleFound));
        }
    }
}
