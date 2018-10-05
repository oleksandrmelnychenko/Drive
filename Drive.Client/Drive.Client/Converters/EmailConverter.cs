using System;
using System.Globalization;
using Xamarin.Forms;

namespace Drive.Client.Converters {
    public class EmailConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string email = value as string;
            return !string.IsNullOrEmpty(email) ? email : "your@email.com";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
