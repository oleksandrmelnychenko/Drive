using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace Drive.Client.Helpers.Localize {
    public class ResourceLoader {
        /// 
        /// TODO: dispose unused strings...!?
        /// 

        public static readonly string APP_STRINGS_PATH = "Drive.Client.Resources.Resx.AppStrings";

        private readonly CultureInfo _defaultCulture = new CultureInfo("uk-UA");
        private readonly ResourceManager _manager;
        private readonly List<StringResource> _resources = new List<StringResource>();

        private ResourceLoader(ResourceManager resourceManager) {
            _manager = resourceManager;
            _cultureInfo = _defaultCulture;

            Instance = this;
        }

        public static ResourceLoader Instance { get; private set; }

        private CultureInfo _cultureInfo;
        public CultureInfo CultureInfo {
            get => _cultureInfo;
            set {
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

        public static void Init() {
            new ResourceLoader(new ResourceManager(APP_STRINGS_PATH, IntrospectionExtensions.GetTypeInfo(typeof(ResourceLoader)).Assembly));
        }

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

        //public StringResource GetString(string resourceName) {
        //    ///
        //    /// TODO: if string also exist return it???
        //    /// 

        //    string stringRes = _manager.GetString(resourceName, _cultureInfo);

        //    StringResource stringResource = new StringResource(resourceName, stringRes);
        //    _resources.Add(stringResource);

        //    return stringResource;
        //}
    }
}
