using Drive.Client.Controls;
using Drive.Client.iOS.Renderers;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedContentView), typeof(ExtendedContentViewRenderer))]
namespace Drive.Client.iOS.Renderers {
    public class ExtendedContentViewRenderer : VisualElementRenderer<ExtendedContentView> {
        private ExtendedContentView _element;

        protected override void OnElementChanged(ElementChangedEventArgs<ExtendedContentView> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {

                _element = e.NewElement as ExtendedContentView;
                SetupLayer((int)_element.BorderThickness, _element.CornerRadius);
            }
        }

        private void SetupLayer(int borderWidth, nfloat borderRadius) {
            Layer.CornerRadius = borderRadius;

            if (Element.BackgroundColor != Color.Default) {
                Layer.BackgroundColor = Element.BackgroundColor.ToUIColor().CGColor;
            }
            else {
                Layer.BackgroundColor = UIColor.White.CGColor;
            }

            if (Element.BorderColor != Color.Default) {
                Layer.BorderColor = Element.BorderColor.ToCGColor();
                Layer.BorderWidth = borderWidth;
            }

            Layer.RasterizationScale = UIScreen.MainScreen.Scale;
            Layer.ShouldRasterize = true;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ExtendedContentView.BorderColorProperty.PropertyName ||
                e.PropertyName == ExtendedContentView.BorderThicknessProperty.PropertyName ||
                e.PropertyName == ExtendedContentView.CornerRadiusProperty.PropertyName) {
                if (Element != null) {
                    SetupLayer((int)Element.BorderThickness, Element.CornerRadius);
                }
            }
        }
    }
}