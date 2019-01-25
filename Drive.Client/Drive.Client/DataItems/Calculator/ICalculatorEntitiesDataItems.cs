using Drive.Client.Models.DataItems.Calculator;
using System.Collections.Generic;

namespace Drive.Client.DataItems.Calculator {
    public interface ICalculatorEntitiesDataItems {

        List<CurrencyDataItem> GetCurrencyDataItems();
    }
}
