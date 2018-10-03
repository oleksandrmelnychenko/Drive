using Drive.Client.Models.EntityModels.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.Identity {
    public interface IIdentityService {
        Task<PhoneNumberAvailabilty> CheckPhoneNumberAvailabiltyAsync(string phoneNumber, CancellationToken cancellationToken = default(CancellationToken));

        Task<UserNameAvailability> CheckUserNameAvailabiltyAsync(string userNmae, CancellationToken cancellationToken = default(CancellationToken));
    }
}
