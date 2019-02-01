using System;
using Drive.Client.Models.Calculator;

namespace Drive.Client.Services.Customs {
    public class CustomsService : ICustomsService {

        private const long GASOLINE_BID = 50;

        private const long DIESEL_BID = 75;

        private const decimal PREFERENTIAL_MULTIPLIER = 0.5M;

        public CustomsResult CalculateCustoms(CarCustoms carCustoms) {
            CustomsResult customsResult = new CustomsResult();

            decimal bondedCarCost = carCustoms.Price;

            decimal importDuty = CalculateImportDuty(bondedCarCost);

            decimal exciseDuty = CalculateExciseDuty(carCustoms.EngineType, carCustoms.Year, carCustoms.EngineCap);
            if (carCustoms.PreferentialExcise) {
                exciseDuty = CalculatePreferentialExcise(exciseDuty);
            }

            decimal vat = CalculateVat(bondedCarCost, importDuty, exciseDuty);

            decimal customsClearanceCosts = CalculateCustomsClearanceCosts(importDuty, exciseDuty, vat);

            decimal clearedCarsCost = CalculateClearedCarsCost(bondedCarCost, customsClearanceCosts);

            customsResult.BondedCarCost = bondedCarCost.ToString();
            customsResult.ImportDuty = importDuty.ToString();
            customsResult.ExciseDuty = exciseDuty.ToString();
            customsResult.Vat = vat.ToString();
            customsResult.CustomsClearanceCosts = customsClearanceCosts.ToString();
            customsResult.ClearedCarsCost = clearedCarsCost.ToString();

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
        private decimal CalculateExciseDuty(string engineType, decimal year, decimal engineCap) {
            decimal exciseDuty = default(decimal);

            decimal engineMultiplier = CalculateEngineMultiplier(engineCap);
            if (engineType == "Gasoline") {
                exciseDuty = engineMultiplier * GASOLINE_BID * year;
            } else {
                exciseDuty = engineMultiplier * DIESEL_BID * year;
            }

            return exciseDuty;
        }

        // Engine multiplier from engine capacity.
        private decimal CalculateEngineMultiplier(decimal engineCap) {
            return engineCap / 1000;
        }

        // Import duty 10% from bonded car cost.
        private decimal CalculateImportDuty(decimal bondedCarCost) {
            return bondedCarCost / 100 * 10;
        }
    }
}
