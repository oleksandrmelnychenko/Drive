using Drive.Client.Models.DataItems;
using Drive.Client.Models.EntityModels.TODO;
using Drive.Client.ViewModels.Popups;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms.Internals;

namespace Drive.Client.Factories.ObjectToSelectorDataItem {
    public class ObjectToSelectorDataItemFactory : IObjectToSelectorDataItemFactory {

        private static readonly string USD_CURRENCY_ICON_PATH = "resource://Drive.Client.Resources.Images.Gear.svg";
        private static readonly string EURO_CURRENCY_ICON_PATH = "resource://Drive.Client.Resources.Images.Gear.svg";

        /// <summary>
        /// TODO: temporary implementation
        /// </summary>
        public List<CommonDataItem<Currency>> BuildCommonDataItems(IEnumerable<Currency> data, string titleStrinFormat = "") {
            List<CommonDataItem<Currency>> result = null;

            try {
                result = new List<CommonDataItem<Currency>>();

                data.ForEach(currency => {
                    CommonDataItem<Currency> item = new CommonDataItem<Currency>() {
                        Data = currency,
                        IconPath = currency == Currency.Euro ? EURO_CURRENCY_ICON_PATH : USD_CURRENCY_ICON_PATH,
                        Titile = currency.ToString().ToUpper()
                    };
                    ((IPopupSelectionItem)item).Title = item.Titile;

                    result.Add(item);
                });
            }
            catch (Exception exc) {
                Crashes.TrackError(exc);
                Debugger.Break();
            }

            return result;
        }

        /// <summary>
        /// TODO: temporary implementation
        /// </summary>
        public List<CommonDataItem<EngineType>> BuildCommonDataItems(IEnumerable<EngineType> data, string titleStrinFormat = "") {
            List<CommonDataItem<EngineType>> result = null;

            try {
                result = new List<CommonDataItem<EngineType>>();

                data.ForEach(engineType => {
                    CommonDataItem<EngineType> item = new CommonDataItem<EngineType>() {
                        Data = engineType,
                        Titile = engineType.ToString().ToUpper()
                    };
                    ((IPopupSelectionItem)item).Title = item.Titile;

                    result.Add(item);
                });
            }
            catch (Exception exc) {
                Crashes.TrackError(exc);
                Debugger.Break();
            }

            return result;
        }

        /// <summary>
        /// TODO: temporary implementation
        /// </summary>
        public List<CommonDataItem<VehicleType>> BuildCommonDataItems(IEnumerable<VehicleType> data, string titleStrinFormat = "") {
            List<CommonDataItem<VehicleType>> result = null;

            try {
                result = new List<CommonDataItem<VehicleType>>();

                data.ForEach(vehicleType => {
                    CommonDataItem<VehicleType> item = new CommonDataItem<VehicleType>() {
                        Data = vehicleType,
                        Titile = vehicleType.ToString().ToUpper()
                    };
                    ((IPopupSelectionItem)item).Title = item.Titile;

                    result.Add(item);
                });
            }
            catch (Exception exc) {
                Crashes.TrackError(exc);
                Debugger.Break();
            }

            return result;
        }

        /// <summary>
        /// TODO: temporary implementation
        /// </summary>
        public List<CommonDataItem<string>> BuildCommonDataItems(IEnumerable<string> data, string titleStrinFormat = "") {
            List<CommonDataItem<string>> result = null;

            try {
                result = new List<CommonDataItem<string>>();

                data.ForEach(singleDataValue => {
                    CommonDataItem<string> item = new CommonDataItem<string>() {
                        Data = singleDataValue,
                        Titile = singleDataValue.ToString().ToUpper()
                    };
                    ((IPopupSelectionItem)item).Title = item.Titile;

                    result.Add(item);
                });
            }
            catch (Exception exc) {
                Crashes.TrackError(exc);
                Debugger.Break();
            }

            return result;
        }

        /// <summary>
        /// TODO: temporary implementation
        /// </summary>
        public List<CommonDataItem<double>> BuildCommonDataItems(IEnumerable<double> data, string titleStrinFormat = "") {
            List<CommonDataItem<double>> result = null;

            try {
                result = new List<CommonDataItem<double>>();

                data.ForEach(singleDataValue => {
                    CommonDataItem<double> item = new CommonDataItem<double>() {
                        Data = singleDataValue,
                        Titile = singleDataValue.ToString(titleStrinFormat).ToUpper()
                    };
                    ((IPopupSelectionItem)item).Title = item.Titile;

                    result.Add(item);
                });
            }
            catch (Exception exc) {
                Crashes.TrackError(exc);
                Debugger.Break();
            }

            return result;
        }
    }
}
