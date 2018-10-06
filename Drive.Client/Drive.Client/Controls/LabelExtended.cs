using System.Diagnostics;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Drive.Client.Controls {
    public class LabelExtended : Label {

        public static readonly BindableProperty LetterSpacingProperty = BindableProperty.Create(
            nameof(LetterSpacing),
            typeof(float),
            typeof(LabelExtended),
            defaultValue: (Device.RuntimePlatform == Device.Android) ? .05F : 1f);

        public static readonly BindableProperty LetterCaseTransformProperty = BindableProperty.Create(
            nameof(LetterCaseTransform),
            typeof(LetterTransformation),
            typeof(LabelExtended),
            defaultValue: LetterTransformation.NoTransform,
            propertyChanged: (BindableObject bindable, object oldValue, object newValue) => { if (bindable is LabelExtended declarer) declarer.OnLetterSpacing(); });

        public float LetterSpacing {
            get => (float)GetValue(LetterSpacingProperty);
            set => SetValue(LetterSpacingProperty, value);
        }

        public LetterTransformation LetterCaseTransform {
            get => (LetterTransformation)GetValue(LetterCaseTransformProperty);
            set => SetValue(LetterCaseTransformProperty, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if (propertyName == Label.TextProperty.PropertyName) {
                OnLetterSpacing();
            }
            else if (propertyName == LetterCaseTransformProperty.PropertyName) {
                OnLetterSpacing();
            }
        }

        private void OnLetterSpacing() {
            //switch (LetterCaseTransform) {
            //    case LetterTransformation.NoTransform:
            //        break;
            //    case LetterTransformation.ToUpperCase:
            //        Text = Text?.ToUpper();
            //        break;
            //    case LetterTransformation.ToLowerCase:
            //        Text = Text?.ToLower();
            //        break;
            //    default:
            //        Debugger.Break();
            //        break;
            //}
        }
    }

    public enum LetterTransformation {
        NoTransform,
        ToUpperCase,
        ToLowerCase
    }
}
