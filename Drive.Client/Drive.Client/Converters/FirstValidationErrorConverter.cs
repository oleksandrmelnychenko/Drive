using Drive.Client.Helpers.Localize;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace Drive.Client.Converters {
    public class FirstValidationErrorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            ICollection<StringResource> errors = value as ICollection<StringResource>; 
            return errors != null && errors.Count > 0 ? errors.ElementAt(0).Value : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
