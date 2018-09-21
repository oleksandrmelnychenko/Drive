using Xamarin.Forms;

namespace Drive.Client.Controls {
    public class ExtendedContentView : ContentView {
        public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(
            propertyName: nameof(BorderThickness),
            returnType: typeof(float),
            declaringType: typeof(ExtendedContentView),
            defaultValue: default(float));

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
            propertyName: nameof(BorderColor),
            returnType: typeof(Color),
            declaringType: typeof(ExtendedContentView),
            defaultValue: default(Color));

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            propertyName: nameof(CornerRadius),
            returnType: typeof(int),
            declaringType: typeof(ExtendedContentView),
            defaultValue: default(int));

        public int CornerRadius {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public float BorderThickness {
            get { return (float)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        public Color BorderColor {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint) {
            return base.OnMeasure(widthConstraint, heightConstraint);
        }
    }
}
