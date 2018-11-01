using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.Models.Rest.EndPoints {
    public class IdentityEndpoints {

        private const string PHONENUMBER_AVAILABILITY_API_KEY = "api/v1/user/check/phone?phone={0}";

        private const string USER_NAME_AVAILABILITY_API_KEY = "api/v1/user/check/name?name={0}";

        private const string SIGNUP_API_KEY = "api/v1/user/signup";

        private const string SIGNIN_API_KEY = "api/v1/user/signin";

        private const string LOG_OUT_API_KEY = "api/v1/user/logout";

        private const string CHANGE_PHONENUMBER_API_KEY = "api/v1/user/update/phone?phone={0}";

        private const string CHANGE_USERNAME_API_KEY = "api/v1/user/update/name?name={0}";

        private const string CHANGR_EMAIL_API_KEY = "api/v1/user/update/email?email={0}";

        private const string UPLOAD_USER_AVATAR_API_KEY = "api/v1/user/avatar/upload";

        private const string UPDATE_PASSWORD_API_KEY = "api/v1/user/update/password?newPassword={0}&currentPassword={1}";

        private const string CAN_USER_CHANGE_FORGOTTEN_PASSWORD_API_KEY = "api/v1/user/canchange/forgotten/password?phone={0}&name={1}";

        private const string FORGOT_PASSWORD_API_KEY = "api/v1/user/forgotpassword?phone={0}&name={1}&password={2}";

        private const string GET_USER_API_KEY = "api/v1/user/get";

        private const string IS_CURRENT_PASSWORD_EXIST = "api/v1/user/check/password?currentPassword={0}";

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

        public string SignUpEndPoint { get; private set; }

        public string SignInEndPoint { get; private set; }

        public string LogOutEndPoint { get; private set; }

        public string ChangePhoneNumberEndPoint { get; private set; }

        public string ChangeUserNameEndPoint { get; private set; }

        public string ChangeEmailEndPoint { get; private set; }

        public string UploadUserAvatarEndpoint { get; private set; }

        public string ChangePasswordEndPoint { get; private set; }

        public string CanUserChangeForgottenPasswordEndPoint { get; private set; }

        public string ForgotPasswordEndPoint { get; private set; }

        public string GetUserEndPoint { get; private set; }

        public string IsCurrentPasswordExist { get; private set; }

        private void UpdateEndpoint(string baseEndpoint) {
            CheckPhoneNumberEndPoint = $"{baseEndpoint}/{PHONENUMBER_AVAILABILITY_API_KEY}";
            CheckUserNameEndpoint = $"{baseEndpoint}/{USER_NAME_AVAILABILITY_API_KEY}";
            SignUpEndPoint = $"{baseEndpoint}/{SIGNUP_API_KEY}";
            SignInEndPoint = $"{baseEndpoint}/{SIGNIN_API_KEY}";
            LogOutEndPoint = $"{baseEndpoint}/{LOG_OUT_API_KEY}";
            ChangePhoneNumberEndPoint = $"{baseEndpoint}/{CHANGE_PHONENUMBER_API_KEY}";
            ChangeUserNameEndPoint = $"{baseEndpoint}/{CHANGE_USERNAME_API_KEY}";
            ChangeEmailEndPoint = $"{baseEndpoint}/{CHANGR_EMAIL_API_KEY}";
            UploadUserAvatarEndpoint = $"{baseEndpoint}/{UPLOAD_USER_AVATAR_API_KEY}";
            ChangePasswordEndPoint = $"{baseEndpoint}/{UPDATE_PASSWORD_API_KEY}";
            CanUserChangeForgottenPasswordEndPoint = $"{baseEndpoint}/{CAN_USER_CHANGE_FORGOTTEN_PASSWORD_API_KEY}";
            ForgotPasswordEndPoint = $"{baseEndpoint}/{FORGOT_PASSWORD_API_KEY}";
            GetUserEndPoint = $"{baseEndpoint}/{GET_USER_API_KEY}";
            IsCurrentPasswordExist = string.Format("{0}/{1}", baseEndpoint, IS_CURRENT_PASSWORD_EXIST);
        }
    }
}
