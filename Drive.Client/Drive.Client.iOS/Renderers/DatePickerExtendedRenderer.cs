using Drive.Client.Controls;
using Drive.Client.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DatePickerExtended), typeof(DatePickerExtendedRenderer))]
namespace Drive.Client.iOS.Renderers {
    public class DatePickerExtendedRenderer : DatePickerRenderer {

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e) {
            base.OnElementChanged(e);

            if (Control != null) {
                DisableNativeBorder();
            }
        }

        private void DisableNativeBorder() {
            Control.BorderStyle = UIKit.UITextBorderStyle.None;
        }
    }
}