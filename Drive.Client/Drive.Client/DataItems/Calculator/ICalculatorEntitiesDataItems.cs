using Drive.Client.Models.DataItems.Calculator;
using Drive.Client.Models.EntityModels;
using System.Collections.Generic;

namespace Drive.Client.DataItems.Calculator {
    public interface ICalculatorEntitiesDataItems {

        List<CalculatorDataItemBase<Currency>> GetCurrencyDataItems();

        List<CalculatorDataItemBase<EngineType>> GetEngineTypesDataItems();
    }
}
