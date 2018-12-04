using Drive.Client.Controls;
using Drive.Client.iOS.Renderers;
using Foundation;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EditorExtended), typeof(EditorExtendedRenderer))]
namespace Drive.Client.iOS.Renderers {
    public class EditorExtendedRenderer : EditorRenderer {

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e) {
            base.OnElementChanged(e);

            if (Control != null && Element != null) {
                UpdateLetterSpacing();
            }
        }

        private void UpdateLetterSpacing() {
            if (Element != null) {
                try {
                    Control.AttributedText = new NSAttributedString(Control.Text, kerning: ((EditorExtended)Element).LetterSpacing);
                }
                catch (System.Exception exc) {
                    string message = exc.Message;

                    Debugger.Break();
                }
            }
        }
    }
}