using Android.Content;
using Android.Graphics.Drawables;
using Drive.Client.Controls;
using Drive.Client.Droid.Renderers;
using Drive.Client.Droid.Renderers.Helpers;
using Drive.Client.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EditorExtended), typeof(EditorExtendedRenderer))]
namespace Drive.Client.Droid.Renderers {
    public class EditorExtendedRenderer : EditorRenderer {

        public EditorExtendedRenderer(Context context)
            : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e) {
            base.OnElementChanged(e);

            if (Control != null && Element != null) {
                RemoveUnderscore();
                UpdateLetterSpacing();
            }
        }

        private void RemoveUnderscore() {
            if (Control != null && Element != null) {
                Control.Background = new ColorDrawable(BaseSingleton<ValuesResolver>.Instance.ResolveNativeColor(Element.BackgroundColor));
            }
        }

        private void UpdateLetterSpacing() {
            if (Element == null) {
                return;
            }

            Control.LetterSpacing = ((EditorExtended)Element).LetterSpacing;
        }
    }
}