using System.ComponentModel;
using System.Diagnostics;
using Drive.Client.Controls;
using Drive.Client.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DatePickerExtended), typeof(DatePickerExtendedRenderer))]
namespace Drive.Client.iOS.Renderers {
    public class DatePickerExtendedRenderer : DatePickerRenderer {

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e) {
            base.OnElementChanged(e);

            if (Control != null) {
                DisableNativeBorder();
                ResolveCalendarCulture();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == DatePickerExtended.CalendarCultureProperty.PropertyName) {
                ResolveCalendarCulture();
            }
        }

        private void DisableNativeBorder() {
            Control.BorderStyle = UIKit.UITextBorderStyle.None;
        }

        private void ResolveCalendarCulture() {
            try {
                UIDatePicker calendar = (UIDatePicker)Control.InputView;

                NSLocale nSLocale = ((DatePickerExtended)Element).CalendarCulture != null ? new NSLocale(((DatePickerExtended)Element).CalendarCulture.Name.Substring(0, 2)) : new NSLocale("en");

                calendar.Locale = nSLocale;
            }
            catch (System.Exception exc) {
                Debugger.Break();
            }
        }
    }
}