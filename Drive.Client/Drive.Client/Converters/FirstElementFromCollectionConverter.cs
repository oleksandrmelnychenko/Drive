using Drive.Client.Helpers.Localize;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace Drive.Client.Converters {
    class FirstElementFromCollectionConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is ICollection<StringResource> collection ? collection.FirstOrDefault()?.Value : null;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new InvalidOperationException("FirstElementFromCollectionConverter.ConvertBack");
    }
}
