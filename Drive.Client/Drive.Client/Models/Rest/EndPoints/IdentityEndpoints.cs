using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.Models.Rest.EndPoints {
    public class IdentityEndpoints {

        private const string PHONE_NUMBER_AVAILABILITY_API_KEY = "api/v1/user/check/phone?phone={0}";

        private const string USER_NAME_AVAILABILITY_API_KEY = "api/v1/user/check/name?name={0}";

        private const string SIGNUP_API_KEY = "api/v1/user/signup";

        /// <summary>
        ///     ctor().
        /// </summary>
        /// <param name="defaultEndPoint"></param>
        public IdentityEndpoints(string defaultEndPoint) {
            BaseEndpoint = defaultEndPoint;
        }

        string _baseEndpoint;
        public string BaseEndpoint {
            get { return _baseEndpoint; }
            set {
                _baseEndpoint = value;
                UpdateEndpoint(_baseEndpoint);
            }
        }

        public string CheckPhoneNumberEndPoint { get; private set; }

        public string CheckUserNameEndpoint { get; private set; }

        public string SignUpEndPoint { get; set; }

        private void UpdateEndpoint(string baseEndpoint) {
            CheckPhoneNumberEndPoint = $"{baseEndpoint}/{PHONE_NUMBER_AVAILABILITY_API_KEY}";
            CheckUserNameEndpoint = $"{baseEndpoint}/{USER_NAME_AVAILABILITY_API_KEY}";
            SignUpEndPoint = $"{baseEndpoint}/{SIGNUP_API_KEY}";
        }
    }
}
