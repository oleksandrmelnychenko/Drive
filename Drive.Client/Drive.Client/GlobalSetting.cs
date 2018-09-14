using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client {
    public class GlobalSetting {
        public const string DEFAULT_ENDPOINT = "http://31.128.79.4:13828/";

        public static GlobalSetting Instance { get; } = new GlobalSetting();

        public GlobalSetting() {
            BaseEndpoint = DEFAULT_ENDPOINT;
        }

        private string _baseEndpoint;
        public string BaseEndpoint {
            get { return _baseEndpoint; }
            set {
                _baseEndpoint = value;
                UpdateEndpoint(_baseEndpoint);
            }
        }

        private void UpdateEndpoint(string baseEndpoint) {
            
        }
    }
}
