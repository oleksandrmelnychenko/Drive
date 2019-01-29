using Drive.Client.Models.Calculator.TODO;
using Drive.Client.Models.DataItems;
using System.Collections.Generic;

namespace Drive.Client.Factories.ObjectToSelectorDataItem {
    public interface IObjectToSelectorDataItemFactory {

        /// <summary>
        /// TODO: temporary implementation
        /// </summary>
        List<CommonDataItem<Currency>> BuildCommonDataItems(IEnumerable<Currency> data, string titleStringFormat = "");

        /// <summary>
        /// TODO: temporary implementation
        /// </summary>
        List<CommonDataItem<EngineType>> BuildCommonDataItems(IEnumerable<EngineType> data, string titleStringFormat = "");

        /// <summary>
        /// TODO: temporary implementation
        /// </summary>
        List<CommonDataItem<VehicleType>> BuildCommonDataItems(IEnumerable<VehicleType> data, string titleStringFormat = "");

        /// <summary>
        /// TODO: temporary implementation
        /// </summary>
        List<CommonDataItem<string>> BuildCommonDataItems(IEnumerable<string> data, string titleStringFormat = "");

        /// <summary>
        /// TODO: temporary implementation
        /// </summary>
        List<CommonDataItem<double>> BuildCommonDataItems(IEnumerable<double> data, string titleStringFormat = "");
    }
}
