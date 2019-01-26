using Drive.Client.Models.DataItems.Calculator;
using Drive.Client.Models.EntityModels;
using System.Collections.Generic;

namespace Drive.Client.DataItems.Calculator {
    public class CalculatorEntitiesDataItems : ICalculatorEntitiesDataItems {

        /// <summary>
        /// TODO: replace on `STRING SOURCE`
        /// </summary>
        private static readonly string USD_CURRENCY_TITLE = "USD";
        private static readonly string EURO_CURRENCY_TITLE = "EURO";
        private static readonly string DIESEL_ENGINE_TITLE = "DIESEL";
        private static readonly string GASOLINE_ENGINE_TITLE = "GASOLINE";

        private static readonly string USD_CURRENCY_ICON_PATH = "resource://Drive.Client.Resources.Images.Gear.svg";
        private static readonly string EURO_CURRENCY_ICON_PATH = "resource://Drive.Client.Resources.Images.Gear.svg";

        public List<CommonDataItem<Currency>> GetCurrencyDataItems() =>
            new List<CommonDataItem<Currency>>() {
                new CommonDataItem<Currency>() {
                    Data = Currency.USD,
                    Titile = USD_CURRENCY_TITLE,
                    IconPath = USD_CURRENCY_ICON_PATH
                },
                new CommonDataItem<Currency>() {
                    Data = Currency.Euro,
                    Titile = EURO_CURRENCY_TITLE,
                    IconPath = EURO_CURRENCY_ICON_PATH
                }
            };

        public List<CommonDataItem<EngineType>> GetEngineTypesDataItems() =>
            new List<CommonDataItem<EngineType>>() {
                new CommonDataItem<EngineType>() {
                    Data = EngineType.Diesel,
                    Titile = DIESEL_ENGINE_TITLE
                },
                new CommonDataItem<EngineType>() {
                    Data = EngineType.Gasoline,
                    Titile = GASOLINE_ENGINE_TITLE
                }
            };
    }
}
