using Drive.Client.Models.Calculator;
using System.Threading.Tasks;

namespace Drive.Client.Services.Customs {
    public interface ICustomsService {
        Task<CustomsResult> CalculateCustoms(CarCustoms carCustoms);
    }
}
