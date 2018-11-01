using Drive.Client.Helpers.Localize;
using Drive.Client.Resources.Resx;
using Drive.Client.ViewModels.Base;

namespace Drive.Client.ViewModels.Popups {
    public abstract class RequestInfoPopupBaseViewModel : PopupBaseViewModel {

        public static readonly StringResource COMMON_REQUEST_INFO_MAIN_TITLE = ResourceLoader.Instance.GetString(nameof(AppStrings.RequestSent));
        public static readonly StringResource COMMON_REQUEST_INFO_PLAIN_TEXT = ResourceLoader.Instance.GetString(nameof(AppStrings.AnswerReady));
        public static readonly StringResource NO_RESULTS_OUTPUT = ResourceLoader.Instance.GetString(nameof(AppStrings.VehicleRequestGiveNoResults));
        public static readonly StringResource VEHICLE_FOUND_OUTPUT = ResourceLoader.Instance.GetString(nameof(AppStrings.VehicleFound));

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
    }
}
