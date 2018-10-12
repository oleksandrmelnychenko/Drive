using Drive.Client.Helpers;
using Drive.Client.ViewModels.Base;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Drive.Client.Models.Identities {
    [DataContract]
    public class UserProfile : ExtendedBindableObject {

        /// <summary>
        /// Acces token by user.
        /// </summary>
        [DataMember]
        public string AccesToken { get; set; } = string.Empty;       
        /// <summary>
        /// Refresh token by user.
        /// </summary>
        [DataMember]
        public string RefreshToken { get; set; } = string.Empty;
        /// <summary>
        /// Is authentication.
        /// </summary>
        [DataMember]
        public bool IsAuth { get; set; }
        /// <summary>
        /// Email.
        /// </summary>
        [DataMember]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Id.
        /// </summary>
        [DataMember]
        public string NetId { get; set; }
        /// <summary>
        /// User phone number.
        /// </summary>
        [DataMember]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// User name.
        /// </summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// User avatar.
        /// </summary>
        [DataMember]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// Clear user profile.
        /// </summary>
        internal void ClearUserProfile() {
            AccesToken = string.Empty;
            RefreshToken = string.Empty;
            IsAuth = false;
            Email = string.Empty;
            NetId = string.Empty;
            PhoneNumber = string.Empty;
            UserName = string.Empty;
            AvatarUrl = string.Empty;
        }

        internal void SaveChanges() {
            Settings.UserProfile = JsonConvert.SerializeObject(this);
        }
    }
}
