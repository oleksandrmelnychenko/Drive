using System;
using System.ComponentModel;
using Drive.Client.Controls;
using Drive.Client.iOS.Renderers;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryExtended), typeof(EntryExtendedRenderer))]
namespace Drive.Client.iOS.Renderers {
    public class EntryExtendedRenderer : EntryRenderer {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e) {
            base.OnElementChanged(e);

            if (Control != null) {
                DisableNativeBorder();
            }

            if (Element != null) {
                SetLetterSpacingPlaceholder();
            }

            if (e.OldElement != null) {
                // Unsubscribe from event handlers and cleanup any resources
            }

            if (e.NewElement != null) {
                // Configure the control and subscribe to event handlers
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == EntryExtended.LetterSpacingPlaceholderProperty.PropertyName) {
                SetLetterSpacingPlaceholder();
            }else if (e.PropertyName == Entry.PlaceholderProperty.PropertyName) {
                SetLetterSpacingPlaceholder();
            }
        }

        private void SetLetterSpacingText() {
            if (Element is EntryExtended entryExtended) {
                try {
                    Control.AttributedText = new NSAttributedString(Control.Text, kerning: entryExtended.LetterSpacing);
                }
                catch (Exception ex) {
                    throw;
                }
            }
        }

        private void SetLetterSpacingPlaceholder() {
            if (Element is EntryExtended entryExtended) {
                try {
                    Control.AttributedPlaceholder = new NSAttributedString(Control.Placeholder, kerning: entryExtended.LetterSpacingPlaceholder);
                }
                catch (Exception ex) {
                    throw;
                }
            }
        }

        private void DisableNativeBorder() {
            Control.BorderStyle = UIKit.UITextBorderStyle.None;
        }
    }
}