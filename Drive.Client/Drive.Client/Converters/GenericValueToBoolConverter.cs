using System;
using System.Globalization;
using Xamarin.Forms;

namespace Drive.Client.Converters {
    public class GenericValueToBoolConverter<T> : IValueConverter {

        public T TargetValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (value == null || typeof(T) != value.GetType()) ? false : TargetValue.Equals(value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new InvalidOperationException("GenericValueToBoolConverter<T>.ConvertBack");
    }
}
