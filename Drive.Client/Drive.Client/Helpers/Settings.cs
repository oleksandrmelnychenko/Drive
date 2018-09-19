using Drive.Client.Models.Identities;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Drive.Client.Helpers {
    public static class Settings {

        private static ISettings AppSettings => CrossSettings.Current;

        private const string USER_PROFILE = "user_profile";
        private const string LANGUAGE_CODE = "language_code";
        private const string LANGUAGE_NATIVE_NAME = "language_native_name";

        private static readonly string DefaultUserProfile = string.Empty;
        private static readonly string LanguageCode = "en-US";
        private static readonly string LanguageNative = string.Empty;

        public static string UserProfile {
            get => AppSettings.GetValueOrDefault(USER_PROFILE, DefaultUserProfile);
            set => AppSettings.AddOrUpdateValue(USER_PROFILE, value);
        }

        public static string Language {
            get => AppSettings.GetValueOrDefault(LANGUAGE_CODE, LanguageCode);
            set => AppSettings.AddOrUpdateValue(LANGUAGE_CODE, value);
        }

        public static string LanguageNativeName {
            get => AppSettings.GetValueOrDefault(LANGUAGE_NATIVE_NAME, LanguageNative);
            set => AppSettings.AddOrUpdateValue(LANGUAGE_NATIVE_NAME, value);
        }
    }
}
