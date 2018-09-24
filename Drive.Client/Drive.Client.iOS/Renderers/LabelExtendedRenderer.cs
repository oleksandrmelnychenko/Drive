using Drive.Client.Controls;
using Drive.Client.iOS.Renderers;
using Foundation;
using System.ComponentModel;
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
        }

        private void SetLetterSpacing() {
            if (Element is LabelExtended labelExtended) {
                string text = Control.Text;
                NSMutableAttributedString attributedString = new NSMutableAttributedString(text);

                NSString nsKern = new NSString("NSKern");
                NSObject spacing = NSObject.FromObject(labelExtended.LetterSpacing * 10);
                NSRange range = new NSRange(0, text.Length);

                attributedString.AddAttribute(nsKern, spacing, range);
                Control.AttributedText = attributedString;
            }
        }
    }
}