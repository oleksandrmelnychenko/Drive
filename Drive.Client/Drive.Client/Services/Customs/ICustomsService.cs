using Drive.Client.Models.Calculator;

namespace Drive.Client.Services.Customs {
    public interface ICustomsService {
        CustomsResult CalculateCustoms(CarCustoms carCustoms);
    }
}
