using Drive.Client.Models.DataItems.Calculator;
using Drive.Client.Models.EntityModels;
using System.Collections.Generic;

namespace Drive.Client.DataItems.Calculator {
    public class CalculatorEntitiesDataItems : ICalculatorEntitiesDataItems {

        private static readonly string USD_CURRENCY_TITLE = "USD";
        private static readonly string EURO_CURRENCY_TITLE = "Euro";

        private static readonly string USD_CURRENCY_ICON_PATH = "resource://Drive.Client.Resources.Images.Gear.svg";
        private static readonly string EURO_CURRENCY_ICON_PATH = "resource://Drive.Client.Resources.Images.Email.svg";

        public List<CurrencyDataItem> GetCurrencyDataItems() =>
            new List<CurrencyDataItem>() {
                new CurrencyDataItem() {
                    Currency = Currency.USD,
                    Titile = USD_CURRENCY_TITLE,
                    IconPath = USD_CURRENCY_ICON_PATH
                },
                new CurrencyDataItem() {
                    Currency = Currency.Euro,
                    Titile = EURO_CURRENCY_TITLE,
                    IconPath = EURO_CURRENCY_ICON_PATH
                }
            };
    }
}
