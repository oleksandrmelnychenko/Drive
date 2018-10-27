using System;
using System.Globalization;
using Xamarin.Forms;

namespace Drive.Client.Converters {
    public class StringUrlToUrlImageSourceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string stringValue && Uri.IsWellFormedUriString(stringValue, UriKind.Absolute)) {
                return ImageSource.FromUri(new Uri(stringValue));
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new InvalidOperationException("StringUrlToUrlImageSourceConverter.ConvertBack");
    }
}
