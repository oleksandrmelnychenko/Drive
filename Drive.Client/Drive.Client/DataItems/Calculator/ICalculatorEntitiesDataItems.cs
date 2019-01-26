using Drive.Client.Models.DataItems.Calculator;
using Drive.Client.Models.EntityModels;
using System.Collections.Generic;

namespace Drive.Client.DataItems.Calculator {
    public interface ICalculatorEntitiesDataItems {

        List<CommonDataItem<Currency>> GetCurrencyDataItems();

        List<CommonDataItem<EngineType>> GetEngineTypesDataItems();
    }
}
