using Drive.Client.Helpers.AppEvents.Events.Args;
using Drive.Client.Models.Identities.AppInterface.Language;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace Drive.Client.Helpers.Localize {
    public class ResourceLoader : IDisposable {

        public static readonly string UKRAINIAN_LOCALE_ID = "uk-UA";
        public static readonly string ENGLISH_LOCALE_ID = "en-GB";
        public static readonly string APP_STRINGS_PATH = "Drive.Client.Resources.Resx.AppStrings";

        private readonly CultureInfo _defaultCulture = new CultureInfo(UKRAINIAN_LOCALE_ID);
        private readonly ResourceManager _manager;
        private readonly List<StringResource> _resources = new List<StringResource>();

        public ResourceLoader() {
            _manager = new ResourceManager(APP_STRINGS_PATH, IntrospectionExtensions.GetTypeInfo(typeof(ResourceLoader)).Assembly);

            switch (BaseSingleton<GlobalSetting>.Instance.AppInterfaceConfigurations.LanguageInterface.LanguageInterface) {
                case LanguageInterface.Ukrainian:
                    _cultureInfo = new CultureInfo(UKRAINIAN_LOCALE_ID);
                    break;
                case LanguageInterface.English:
                    _cultureInfo = new CultureInfo(ENGLISH_LOCALE_ID);
                    break;
                default:
                    Debugger.Break();
                    break;
            }

            BaseSingleton<GlobalSetting>.Instance.AppMessagingEvents.LanguageEvents.LanguageChanged += OnLanguageEventsLanguageChanged;
        }

        /// <summary>
        /// TODO: remove
        /// </summary>
        /// <param name="resourceManager"></param>
        //private ResourceLoader(ResourceManager resourceManager) {
        //    _manager = resourceManager;
        //    _cultureInfo = _defaultCulture;

        //    Instance = this;
        //}

        /// <summary>
        /// TODO: remove
        /// </summary>
        //public static ResourceLoader Instance { get; private set; }

        private CultureInfo _cultureInfo;
        public CultureInfo CultureInfo {
            get => _cultureInfo;
            set {
                Thread.CurrentThread.CurrentCulture = value != null ? value : _defaultCulture;
                Thread.CurrentThread.CurrentUICulture = value != null ? value : _defaultCulture;

                if (_cultureInfo?.Name != value?.Name) {
                    _cultureInfo = value;

                    foreach (StringResource stringResource in _resources) {
                        stringResource.Value = _manager.GetString(stringResource.Key, _cultureInfo != null ? _cultureInfo : _defaultCulture);
                    }
                }
                else {
                    _cultureInfo = value;
                }
            }
        }

        public StringResource this[string key] {
            get => GetString(key);
        }

        public void Dispose() {
            BaseSingleton<GlobalSetting>.Instance.AppMessagingEvents.LanguageEvents.LanguageChanged -= OnLanguageEventsLanguageChanged;
        }

        /// <summary>
        /// TODO: remove
        /// </summary>
        //public static void Init() {
        //    new ResourceLoader(new ResourceManager(APP_STRINGS_PATH, IntrospectionExtensions.GetTypeInfo(typeof(ResourceLoader)).Assembly));

        //    switch (BaseSingleton<GlobalSetting>.Instance.AppInterfaceConfigurations.LanguageInterface.LanguageInterface) {
        //        case LanguageInterface.Ukrainian:
        //            Instance.CultureInfo = new CultureInfo(UKRAINIAN_LOCALE_ID);
        //            break;
        //        case LanguageInterface.English:
        //            Instance.CultureInfo = new CultureInfo(ENGLISH_LOCALE_ID);
        //            break;
        //        default:
        //            Debugger.Break();
        //            break;
        //    }
        //}

        public StringResource GetString(string resourceName) {

            string stringRes = _manager.GetString(resourceName, _cultureInfo);

            StringResource stringResource = _resources.FirstOrDefault<StringResource>(existingStringResource => existingStringResource.Key == resourceName);

            if (stringResource != null) {
                stringResource.Value = stringRes;
            }
            else {
                stringResource = new StringResource(resourceName, stringRes);
                _resources.Add(stringResource);
            }

            return stringResource;
        }

        private void OnLanguageEventsLanguageChanged(object sender, LanguageChangedArgs e) {
            CultureInfo = e.AppLanguage.Culture;
        }
    }
}
