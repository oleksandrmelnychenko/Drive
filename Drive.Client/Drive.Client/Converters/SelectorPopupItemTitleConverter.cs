using Drive.Client.ViewModels.Popups;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Drive.Client.Converters {
    public class SelectorPopupItemTitleConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is IPopupSelectionItem popupSelectionItem ? popupSelectionItem.Title : "Unsuported type";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new InvalidOperationException("SelectorPopupItemTitleConverter.ConvertBack");
    }
}
