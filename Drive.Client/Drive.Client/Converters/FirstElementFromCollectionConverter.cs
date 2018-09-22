using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace Drive.Client.Converters {
    class FirstElementFromCollectionConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is ICollection<string> collection ? collection.FirstOrDefault() : null;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new InvalidOperationException("FirstElementFromCollectionConverter.ConvertBack");
    }
}
