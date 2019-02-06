using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Drive.Client.Converters {
    public class StringToDecimalConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string strValue && string.IsNullOrEmpty(strValue) && string.IsNullOrWhiteSpace(strValue) ) {
                return 0;
            }
            return value;
        }
    }
}
