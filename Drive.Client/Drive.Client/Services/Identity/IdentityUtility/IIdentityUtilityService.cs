using System.Threading.Tasks;

namespace Drive.Client.Services.Identity.IdentityUtility {
    public interface IIdentityUtilityService {
        Task LogOutAsync();
    }
}
