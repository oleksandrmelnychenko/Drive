using Drive.Client.ViewModels.Base;
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
        /// Is authentication.
        /// </summary>
        [DataMember]
        public bool IsAuth { get; set; }
    }
}
