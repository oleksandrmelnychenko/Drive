using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Drive.Client.Helpers {
    public static class Settings {

        private static ISettings AppSettings => CrossSettings.Current;

        private const string USER_PROFILE = "user_profile";
        private const string ID_URL_BASE = "url_base";

        private static readonly string UrlBaseDefault = GlobalSetting.Instance.BaseEndpoint;

        public static string UserProfile {
            get => AppSettings.GetValueOrDefault(USER_PROFILE, null);
            set => AppSettings.AddOrUpdateValue(USER_PROFILE, value);
        }

        public static string UrlBase {
            get => AppSettings.GetValueOrDefault(ID_URL_BASE, UrlBaseDefault);
            set => AppSettings.AddOrUpdateValue(ID_URL_BASE, value);
        }
    }
}
