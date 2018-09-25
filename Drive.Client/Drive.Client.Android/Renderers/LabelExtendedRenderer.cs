using Android.Content;
using Drive.Client.Controls;
using Drive.Client.Droid.Renderers;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LabelExtended), typeof(LabelExtendedRenderer))]
namespace Drive.Client.Droid.Renderers {
    class LabelExtendedRenderer : LabelRenderer {

        public LabelExtendedRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e) {
            base.OnElementChanged(e);

            SetLetterSpacing();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == LabelExtended.LetterSpacingProperty.PropertyName) {
                SetLetterSpacing();
                UpdateLayout();
            }
        }

        private void SetLetterSpacing() {
            try {
                Control.LetterSpacing = ((LabelExtended)Element).LetterSpacing;
            }
            catch (System.Exception exc) { }
        }
    }
}