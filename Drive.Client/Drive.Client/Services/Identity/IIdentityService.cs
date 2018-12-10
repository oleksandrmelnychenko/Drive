using Drive.Client.Models.Arguments.IdentityAccounting.ChangePassword;
using Drive.Client.Models.Arguments.IdentityAccounting.ForgotPassword;
using Drive.Client.Models.Arguments.IdentityAccounting.Registration;
using Drive.Client.Models.EntityModels.Identity;
using Drive.Client.Models.Medias;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.Identity {
    public interface IIdentityService {

        Task<PhoneNumberAvailabilty> CheckPhoneNumberAvailabiltyAsync(string phoneNumber, CancellationToken cancellationToken = default(CancellationToken));

        Task<UserNameAvailability> CheckUserNameAvailabiltyAsync(string userNmae, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResult> SignUpAsync(RegistrationCollectedInputsArgs collectedInputsArgs, CancellationToken cancellationToken = default(CancellationToken));

        Task<User> UpdatePasswordAsync(ChangePasswordArgs changePasswordArgs, CancellationToken cancellationToken = default(CancellationToken));

        Task<ChangedProfileData> ChangePhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResult> SignInAsync(SignInArgs signInArgsArgs, CancellationToken cancellationToken = default(CancellationToken));

        Task<ChangedProfileData> ChangeUserNameAsync(string userName, CancellationToken cancellationToken = default(CancellationToken));

        Task<string> UploadUserAvatarAsync(PickedImage pickedImage, CancellationToken cancellationToken = default(CancellationToken));

        Task<ChangedProfileData> ChangeEmailAsync(string value, CancellationToken cancellationToken = default(CancellationToken));

        Task<CanChangeForgottenPassword> CanUserChangeForgottenPasswordAsync(string phoneNumber, string name, CancellationToken cancellationToken = default(CancellationToken));

        Task<User> ForgotPasswordAsync(ForgotPasswordArgs forgotPasswordArgs, CancellationToken cancellationToken = default(CancellationToken));

        Task<User> GetUserAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<IsCurrentPasswordExistResponse> IsCurrentPasswordExistAsync(string password, CancellationToken cancellationToken = default(CancellationToken));

        Task LogOutAsync();

        void StartUseUserProfile();
    }
}
