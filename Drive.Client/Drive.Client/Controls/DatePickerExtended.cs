using System.Globalization;
using Xamarin.Forms;

namespace Drive.Client.Controls {
    public class DatePickerExtended : DatePicker {
        public static BindableProperty CalendarCultureProperty = BindableProperty.Create(
            nameof(CalendarCulture),
            typeof(CultureInfo),
            typeof(DatePickerExtended),
            defaultValue: default(CultureInfo));

        public CultureInfo CalendarCulture {
            get => (CultureInfo)GetValue(CalendarCultureProperty);
            set => SetValue(CalendarCultureProperty, value);
        }
    }
}
