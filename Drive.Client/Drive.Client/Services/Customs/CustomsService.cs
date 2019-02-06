using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Drive.Client.Models.Calculator;
using Drive.Client.Models.Calculator.TODO;
using Drive.Client.Services.RequestProvider;
using Xamarin.Forms.Internals;

namespace Drive.Client.Services.Customs {
    public class CustomsService : ICustomsService {

        private readonly IRequestProvider _requestProvider;

        private const long GASOLINE_BID = 50;

        private const long DIESEL_BID = 75;

        private const decimal PREFERENTIAL_MULTIPLIER = 0.5M;

        public CustomsService(IRequestProvider requestProvider) {
            _requestProvider = requestProvider;
        }

        public async Task<CustomsResult> CalculateCustoms(CarCustoms carCustoms) {
            CustomsResult customsResult = new CustomsResult();

            decimal bondedCarCost = carCustoms.Price;

            decimal importDuty = CalculateImportDuty(bondedCarCost);

            decimal exciseDuty = await CalculateExciseDuty(carCustoms.EngineType, carCustoms.Year, carCustoms.EngineCap, carCustoms.Currency);
            if (carCustoms.PreferentialExcise) {
                exciseDuty = CalculatePreferentialExcise(exciseDuty);
            }

            decimal vat = CalculateVat(bondedCarCost, importDuty, exciseDuty);

            decimal customsClearanceCosts = CalculateCustomsClearanceCosts(importDuty, exciseDuty, vat);

            decimal clearedCarsCost = CalculateClearedCarsCost(bondedCarCost, customsClearanceCosts);

            string viewCurrencyType = carCustoms.Currency == Currency.Euro ? "€" : "$";

            customsResult.BondedCarCost = string.Format($"{bondedCarCost:0.##} {viewCurrencyType}");
            customsResult.ImportDuty = string.Format($"{importDuty:0.##} {viewCurrencyType}");
            customsResult.ExciseDuty = string.Format($"{exciseDuty:0.##} {viewCurrencyType}");
            customsResult.Vat = string.Format($"{vat:0.##} {viewCurrencyType}");
            customsResult.CustomsClearanceCosts = string.Format($"{customsClearanceCosts:0.##} {viewCurrencyType}");
            customsResult.ClearedCarsCost = string.Format($"{clearedCarsCost:0.##} {viewCurrencyType}");

            return customsResult;
        }

        // Sum car price and customs clearance costs.
        private decimal CalculateClearedCarsCost(decimal bondedCarCost, decimal customsClearanceCosts) {
            return bondedCarCost + customsClearanceCosts;
        }

        //  Sum import duty and excise duty and vat.
        private decimal CalculateCustomsClearanceCosts(decimal importDuty, decimal exciseDuty, decimal vat) {
            return importDuty + exciseDuty + vat;
        }

        // Vat 20% from sum with bonded car cost and import duty and excise duty.
        private decimal CalculateVat(decimal bondedCarCost, decimal importDuty, decimal exciseDuty) {
            return (bondedCarCost + importDuty + exciseDuty) / 100 * 20;
        }

        // Temporary preferential excise.
        private decimal CalculatePreferentialExcise(decimal exciseDuty) {
            return exciseDuty * PREFERENTIAL_MULTIPLIER; ;
        }

        // Calculate excise duty.
        private async Task<decimal> CalculateExciseDuty(string engineType, decimal year, double engineCap, Currency currency) {
            decimal exciseDuty = default(decimal);

            double engineMultiplier = engineCap;
            if (engineType == "Gasoline") {
                exciseDuty = (decimal)engineMultiplier * GASOLINE_BID * year;
            } else {
                exciseDuty = (decimal)engineMultiplier * DIESEL_BID * year;
            }

            if (currency == Currency.USD) {
                exciseDuty = await ConvertCurrency(exciseDuty);
            }

            return exciseDuty;
        }

        private async Task<decimal> ConvertCurrency(decimal exciseDuty) {
            string usdRate = string.Empty;
            string euroRate = string.Empty;

            try {
                IEnumerable<ExchangeRate> exchangeRates = await _requestProvider.GetAsync<IEnumerable<ExchangeRate>>("https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5");

                foreach (var item in exchangeRates) {
                    if (item.Ccy == "USD") {
                        usdRate = item.Sale;
                    }
                    if (item.Ccy == "EUR") {
                        euroRate = item.Sale;
                    }
                }

                if (decimal.TryParse(usdRate, out decimal usd) && decimal.TryParse(euroRate, out decimal euro)) {
                    decimal difference = Math.Round(euro / usd, 2);
                    exciseDuty = difference * exciseDuty;
                }
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR: ---------{ex.Message}");
                Debugger.Break();
            }

            return exciseDuty;
        }

        // Import duty 10% from bonded car cost.
        private decimal CalculateImportDuty(decimal bondedCarCost) {
            return bondedCarCost / 100 * 10;
        }
    }
}
