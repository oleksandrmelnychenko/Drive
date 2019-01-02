using Android.Content;
using Android.Views;
using Drive.Client.Controls;
using Drive.Client.Droid.Renderers;
using System;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryExtended), typeof(EntryExtendedRenderer))]
namespace Drive.Client.Droid.Renderers {
    internal class EntryExtendedRenderer : EntryRenderer {

        private const GravityFlags DefaultGravity = GravityFlags.CenterVertical;

        private BorderRenderer _renderer;

        public EntryExtendedRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e) {
            base.OnElementChanged(e);
            if (e.OldElement != null || this.Element == null) {
                return;
            }

            Control.Gravity = DefaultGravity;

            EntryExtended entryEx = Element as EntryExtended;

            UpdateLetterSpacingPlaceholder(entryEx);
            UpdateBackground(entryEx);
            UpdatePadding(entryEx);
            UpdateTextAlighnment(entryEx);
        }

        private void UpdateLetterSpacingPlaceholder(EntryExtended entryEx) {
            try {
                if (entryEx == null) return;

                Control.LetterSpacing = ((EntryExtended)Element).LetterSpacingPlaceholder;
            }
            catch (Exception ex) {
                Debugger.Break();
                System.Diagnostics.Debug.WriteLine($"ERROR: {ex.Message}");
            }
        }

        private void UpdateLetterSpacing(EntryExtended entryEx) {
            try {
                if (entryEx == null) return;

                Control.LetterSpacing = ((EntryExtended)Element).LetterSpacing;
            }
            catch (Exception ex) {
                Debugger.Break();
                System.Diagnostics.Debug.WriteLine($"ERROR: {ex.Message}");
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            try {
                if (Element == null) {
                    return;
                }

                EntryExtended entryEx = Element as EntryExtended;

                if (e.PropertyName == EntryExtended.BorderWidthProperty.PropertyName ||
                    e.PropertyName == EntryExtended.BorderColorProperty.PropertyName ||
                    e.PropertyName == EntryExtended.BorderRadiusProperty.PropertyName ||
                    e.PropertyName == EntryExtended.BackgroundColorProperty.PropertyName) {
                    UpdateBackground(entryEx);
                } else if (e.PropertyName == EntryExtended.LeftPaddingProperty.PropertyName ||
                      e.PropertyName == EntryExtended.RightPaddingProperty.PropertyName) {
                    UpdatePadding(entryEx);
                } else if (e.PropertyName == Entry.HorizontalTextAlignmentProperty.PropertyName) {
                    UpdateTextAlighnment(entryEx);
                } else if (e.PropertyName == Entry.TextProperty.PropertyName) {
                    UpdateLetterSpacing(entryEx);

                    if (string.IsNullOrEmpty(entryEx.Text)) {
                        UpdateLetterSpacingPlaceholder(entryEx);
                    }
                } else if (e.PropertyName == EntryExtended.LetterSpacingPlaceholderProperty.PropertyName) {
                    UpdateLetterSpacingPlaceholder(entryEx);
                }
            }
            catch (Exception ex) {
                Debugger.Break();
                System.Diagnostics.Debug.WriteLine($"ERROR: {ex.Message}");
            }
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
            try {
                if (disposing) {
                    if (_renderer != null) {
                        _renderer.Dispose();
                        _renderer = null;
                    }
                }
            }
            catch (Exception ex) {
                Debugger.Break();
                System.Diagnostics.Debug.WriteLine($"ERROR: {ex.Message}");
            }
        }

        private void UpdateBackground(EntryExtended entryEx) {
            try {
                _renderer = new BorderRenderer();

                Control.Background = _renderer.GetBorderBackground(entryEx.BorderColor, entryEx.BackgroundColor, entryEx.BorderWidth, entryEx.BorderRadius);
            }
            catch (Exception ex) {
                Debugger.Break();
                System.Diagnostics.Debug.WriteLine($"ERROR: {ex.Message}");
            }
            if (_renderer != null) {
                _renderer.Dispose();
                _renderer = null;
            }
            //Control.Background = new Android.Graphics.Drawables.ColorDrawable(BaseSingleton<ValuesResolver>.Instance.ResolveNativeColor(entryEx.BackgroundColor));
            //Control.Background.SetColorFilter(BaseSingleton<ValuesResolver>.Instance.ResolveNativeColor(entryEx.BackgroundColor), PorterDuff.Mode.SrcAtop);
            //Control.Background.SetColorFilter(Android.Graphics.Color.ParseColor("#fefefe"), PorterDuff.Mode.SrcAtop);
        }

        private void UpdatePadding(EntryExtended entryEx) {
            try {
                Control.SetPadding((int)Context.ToPixels(entryEx.LeftPadding), 0,
                    (int)Context.ToPixels(entryEx.RightPadding), 0);
            }
            catch (Exception ex) {
                Debugger.Break();
                System.Diagnostics.Debug.WriteLine($"ERROR: {ex.Message}");
            }
        }

        private void UpdateTextAlighnment(EntryExtended entryEx) {
            try {
                GravityFlags gravity = DefaultGravity;

                switch (entryEx.HorizontalTextAlignment) {
                    case Xamarin.Forms.TextAlignment.Start:
                        gravity |= GravityFlags.Start;
                        break;
                    case Xamarin.Forms.TextAlignment.Center:
                        gravity |= GravityFlags.CenterHorizontal;
                        break;
                    case Xamarin.Forms.TextAlignment.End:
                        gravity |= GravityFlags.End;
                        break;
                }

                Control.Gravity = gravity;
            }
            catch (Exception ex) {
                Debugger.Break();
                System.Diagnostics.Debug.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}