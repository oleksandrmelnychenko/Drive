using Xamarin.Forms;

namespace Drive.Client.Controls {
    public class EditorExtended : Editor {

        public static readonly BindableProperty LetterSpacingProperty = BindableProperty.Create(
           propertyName: nameof(LetterSpacing),
           returnType: typeof(float),
           declaringType: typeof(EditorExtended),
           defaultValue: (Device.RuntimePlatform == Device.Android) ? .05F : 1f);

        public float LetterSpacing {
            get { return (float)GetValue(LetterSpacingProperty); }
            set { SetValue(LetterSpacingProperty, value); }
        }
    }
}
