using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Drive.Client.Helpers {
    public static class Settings {

        private static ISettings AppSettings => CrossSettings.Current;

        private const string USER_PROFILE = "user_profile";
        private const string APP_INTERFACE_CONFIGURATIONS = "app_interface_configurations";

        private static readonly string DefaultValueStub = string.Empty;

        public static string UserProfile {
            get => AppSettings.GetValueOrDefault(USER_PROFILE, DefaultValueStub);
            set => AppSettings.AddOrUpdateValue(USER_PROFILE, value);
        }

        public static string AppInterfaceConfigurations {
            get => AppSettings.GetValueOrDefault(APP_INTERFACE_CONFIGURATIONS, DefaultValueStub);
            set => AppSettings.AddOrUpdateValue(APP_INTERFACE_CONFIGURATIONS, value);
        }
    }
}
