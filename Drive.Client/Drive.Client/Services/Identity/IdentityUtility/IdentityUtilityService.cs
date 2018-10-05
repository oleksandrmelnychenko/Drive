using Drive.Client.Helpers;
using Drive.Client.Services.Navigation;

namespace Drive.Client.Services.Identity.IdentityUtility {
    public class IdentityUtilityService : IIdentityUtilityService {

        private readonly INavigationService _navigationService;

        /// <summary>
        ///     ctor().
        /// </summary>
        public IdentityUtilityService(INavigationService navigationService) {
            _navigationService = navigationService;
        }

        public async void LogOut() {
            BaseSingleton<GlobalSetting>.Instance.UserProfile.ClearUserProfile();

            await _navigationService.InitializeAsync();
        }
    }
}
