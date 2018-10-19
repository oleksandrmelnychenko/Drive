using Android.Content;
using Drive.Client.Controls;
using Drive.Client.Droid.Renderers;
using Java.Util;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DatePickerExtended), typeof(DatePickerExtendedRenderer))]
namespace Drive.Client.Droid.Renderers {
    public class DatePickerExtendedRenderer : DatePickerRenderer {

        public DatePickerExtendedRenderer(Context context)
            : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e) {
            base.OnElementChanged(e);

            if (Control != null) {
                ///
                /// Removes underline
                /// 
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);

                ///
                /// Input text layouting adjustment
                /// 
                Android.Support.V4.View.ViewCompat.SetPaddingRelative(Control, 0, 0, 0, 0);

                ResolveCalendarCulture();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == DatePickerExtended.CalendarCultureProperty.PropertyName) {
                ResolveCalendarCulture();
            }
        }

        private void ResolveCalendarCulture() {
            try {
                Locale targetLocale = ((DatePickerExtended)Element).CalendarCulture != null ? new Locale(((DatePickerExtended)Element).CalendarCulture.Name.Substring(0, 2)) : Locale.English;

                Locale.Default = targetLocale;
                Control.TextLocale = targetLocale;
            }
            catch (System.Exception exc) {
                Debugger.Break();
            }
        }
    }
}