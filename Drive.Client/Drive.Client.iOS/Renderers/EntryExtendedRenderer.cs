using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using CoreGraphics;
using Drive.Client.Controls;
using Drive.Client.iOS.Renderers;
using Foundation;
using UIKit;
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
                SetLetterSpacingText();
                SetLetterSpacingPlaceholder();
                UpdatePadding();
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
            } else if (e.PropertyName == Entry.PlaceholderProperty.PropertyName) {
                SetLetterSpacingPlaceholder();
            } else if (e.PropertyName == Entry.TextProperty.PropertyName) {
                SetLetterSpacingText();
            } else if (e.PropertyName == EntryExtended.LeftPaddingProperty.PropertyName) {
                UpdatePadding();
            }
        }

        private void UpdatePadding() {
            UIView paddingView = new UIView(new CGRect(0, 0, ((EntryExtended)Element).LeftPadding, 0));
            Control.LeftView = paddingView;
            Control.LeftViewMode = UITextFieldViewMode.Always;
        }

        private void SetLetterSpacingText() {
            if (Element is EntryExtended entryExtended) {
                try {
                    Control.AttributedText = new NSAttributedString(Control.Text, kerning: entryExtended.LetterSpacing);
                }
                catch (Exception ex) {
                    Debugger.Break();
                    Debug.WriteLine($"ERROR: {ex.Message}");
                }
            }
        }

        private void SetLetterSpacingPlaceholder() {
            if (Element is EntryExtended entryExtended) {
                try {
                    if (Control.Placeholder != null) {
                        Control.AttributedPlaceholder = new NSAttributedString(str: Control.Placeholder,
                                                                               font: UIKit.UIFont.SystemFontOfSize((float)entryExtended.FontSize),
                                                                               kerning: entryExtended.LetterSpacingPlaceholder);
                    }
                }
                catch (Exception ex) {
                    Debugger.Break();
                    Debug.WriteLine($"ERROR: {ex.Message}");
                }
            }
        }

        private void DisableNativeBorder() {
            Control.BorderStyle = UIKit.UITextBorderStyle.None;
        }
    }
}