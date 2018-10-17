using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Drive.Client.Converters {
    public sealed class VechicalCountConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) return null;

            long result = (long)value;

            return result.Equals(0) || result > 4 ? "автомобілів" : result.Equals(1) ? "автомобіль" : "автомобіля";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
