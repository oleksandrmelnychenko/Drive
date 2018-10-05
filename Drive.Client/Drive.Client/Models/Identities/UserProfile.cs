﻿using Drive.Client.ViewModels.Base;
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
        public string Email { get; set; }
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
    }
}
