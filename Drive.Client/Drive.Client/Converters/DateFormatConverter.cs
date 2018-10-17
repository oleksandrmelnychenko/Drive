using Drive.Client.Helpers;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Drive.Client.Converters {
    public class DateFormatConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            if (value == null) return null;

            var cultureInfo = new CultureInfo(BaseSingleton<GlobalSetting>.Instance.AppInterfaceConfigurations.LanguageInterface.LocaleId);

            DateTime dt = DateTime.Parse(value.ToString());
            return dt.ToString("MMM/yyyy", cultureInfo).Replace('.', ' ').Replace('/', ' ').ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
