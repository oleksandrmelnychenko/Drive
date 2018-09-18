using System;
using System.Globalization;
using Xamarin.Forms;

namespace Drive.Client.Converters {
    public class BoolToGenericObjectConverter<T> : IValueConverter {

        public T TrueObject { get; set; }

        public T FalseObject { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) {
                return FalseObject;
            }

            return ((bool)value) ? TrueObject : FalseObject;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return ((T)value).Equals(TrueObject);
        }
    }
}
