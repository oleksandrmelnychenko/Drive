using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
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
                SetLetterSpacingText();
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
            } else if (e.PropertyName == Entry.TextProperty.PropertyName) {
                SetLetterSpacingText();
                //var tt = NSLocale.CurrentLocale;
                //var tt = NSLocale.PreferredLanguages[0];
                //var x = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            }
        }

        private void SetLetterSpacingText() {
            if (Element is EntryExtended entryExtended) {
                try {
                    Control.AttributedText = new NSAttributedString(Control.Text, kerning: entryExtended.LetterSpacing);
                }
                catch (Exception ex) {
                    Debugger.Break();
                    throw;
                }
            }
        }

        private void SetLetterSpacingPlaceholder() {
            if (Element is EntryExtended entryExtended) {
                try {
                    Control.AttributedPlaceholder =new NSAttributedString(str: Control.Placeholder,
                                                                          font: UIKit.UIFont.SystemFontOfSize((float)entryExtended.FontSize),
                                                                          kerning: entryExtended.LetterSpacingPlaceholder);
                }
                catch (Exception ex) {
                    Debugger.Break();
                    throw;
                }
            }
        }

        private void DisableNativeBorder() {
            Control.BorderStyle = UIKit.UITextBorderStyle.None;
        }
    }
}