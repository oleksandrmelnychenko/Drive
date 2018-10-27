using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.ViewModels.BottomTabViewModels.Bookmark;
using Drive.Client.Views.Popups;
using System;

namespace Drive.Client.ViewModels.Popups {
    public class RequestInfoPopupViewModel : RequestInfoPopupBaseViewModel {

        public RequestInfoPopupViewModel() {
            MainTitle = COMMON_REQUEST_INFO_MAIN_TITLE;
            PlainOutputText = COMMON_REQUEST_INFO_PLAIN_TEXT;
        }

        public override Type RelativeViewType => typeof(RequestInfoPopupView);

        protected async override void OnIsPopupVisible() {
            base.OnIsPopupVisible();

            if (!IsPopupVisible) {
                await NavigationService.NavigateToAsync<MainViewModel>(new BottomTabIndexArgs { TargetTab = typeof(BookmarkViewModel) });
            }
        }
    }
}
