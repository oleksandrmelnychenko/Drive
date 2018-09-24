using Xamarin.Forms;

namespace Drive.Client.Controls {
    public class LabelExtended : Label {

        public static readonly BindableProperty LetterSpacingProperty = BindableProperty.Create(
            nameof(LetterSpacing),
            typeof(float),
            typeof(LabelExtended),
            defaultValue: .05F);

        public float LetterSpacing {
            get => (float)GetValue(LetterSpacingProperty);
            set => SetValue(LetterSpacingProperty, value);
        }
    }
}
