using Drive.Client.Controls;
using Drive.Client.iOS.Renderers;
using Foundation;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LabelExtended), typeof(LabelExtendedRenderer))]
namespace Drive.Client.iOS.Renderers {
    class LabelExtendedRenderer : LabelRenderer {

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e) {
            base.OnElementChanged(e);

            if (Element != null) {
                SetLetterSpacing();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == LabelExtended.LetterSpacingProperty.PropertyName) {
                SetLetterSpacing();
            }
            else if (e.PropertyName == LabelExtended.TextProperty.PropertyName) {
                SetLetterSpacing();
            }
        }

        private void SetLetterSpacing() {
            if (Element is LabelExtended labelExtended && !string.IsNullOrEmpty(Control.Text)) {
                try {                   
                    Control.AttributedText = new NSAttributedString(Control.Text, kerning: labelExtended.LetterSpacing);
                }
                catch (System.Exception ex) {
                    Debugger.Break();
                    Debug.WriteLine($"ERROR: {ex.Message}");
                }
            }
        }
    }
}