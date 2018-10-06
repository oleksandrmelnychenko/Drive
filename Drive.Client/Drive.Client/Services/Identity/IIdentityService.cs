using Drive.Client.Models.Arguments.IdentityAccounting.Registration;
using Drive.Client.Models.EntityModels.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.Identity {
    public interface IIdentityService {
        Task<PhoneNumberAvailabilty> CheckPhoneNumberAvailabiltyAsync(string phoneNumber, CancellationToken cancellationToken = default(CancellationToken));

        Task<UserNameAvailability> CheckUserNameAvailabiltyAsync(string userNmae, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResult> SignUpAsync(RegistrationCollectedInputsArgs collectedInputsArgs, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResult> SignInAsync(SignInArgs signInArgsArgs, CancellationToken cancellationToken = default(CancellationToken));
    }
}
