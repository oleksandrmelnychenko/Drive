using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Controls {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditorCompounded : ContentView {

        public static readonly BindableProperty LetterSpacingProperty = BindableProperty.Create(
           propertyName: nameof(LetterSpacing),
           returnType: typeof(float),
           declaringType: typeof(EditorCompounded),
           defaultValue: (Device.RuntimePlatform == Device.Android) ? .05F : 1f);

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder),
                typeof(string),
                typeof(EditorCompounded),
                defaultValue: default(string));

        public static readonly BindableProperty InputTextProperty =
            BindableProperty.Create(nameof(InputText),
                typeof(string),
                typeof(EditorCompounded),
                defaultValue: default(string),
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: (BindableObject bindable, object oldValue, object newValue) => {
                    if (bindable is EditorCompounded declarer) {
                        declarer.ResolvePlaceholderVisibility();
                    }
                });

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor),
                typeof(Color),
                typeof(EditorCompounded),
                defaultValue: Color.Black);

        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create(nameof(PlaceholderColor),
                typeof(Color),
                typeof(EditorCompounded),
                defaultValue: Color.Gray);

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily),
                typeof(string),
                typeof(EditorCompounded),
                defaultValue: default(string));

        public static readonly BindableProperty PlaceholderYOffsetProperty =
            BindableProperty.Create(nameof(PlaceholderYOffset),
                typeof(double),
                typeof(EditorCompounded),
                defaultValue: PLACEHOLDER_DEFAULT_Y_OFFSET);

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize),
                typeof(double),
                typeof(EditorCompounded),
                defaultValue: 14.0);

        private static readonly double PLACEHOLDER_DEFAULT_X_OFFSET = 6.0;
        private static readonly double PLACEHOLDER_DEFAULT_Y_OFFSET = 10.0;

        public EditorCompounded() {
            InitializeComponent();

            _mainInput_EditorExtended.SetBinding(EditorExtended.LetterSpacingProperty, new Binding(nameof(LetterSpacing), source: this));
            _mainInput_EditorExtended.SetBinding(EditorExtended.TextProperty, new Binding(nameof(InputText), source: this));
            _mainInput_EditorExtended.SetBinding(EditorExtended.TextColorProperty, new Binding(nameof(TextColor), source: this));
            _mainInput_EditorExtended.SetBinding(EditorExtended.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
            _mainInput_EditorExtended.SetBinding(EditorExtended.FontSizeProperty, new Binding(nameof(FontSize), source: this));

            _placeholder_LabelExtended.SetBinding(LabelExtended.LetterSpacingProperty, new Binding(nameof(LetterSpacing), source: this));
            _placeholder_LabelExtended.SetBinding(LabelExtended.TextProperty, new Binding(nameof(Placeholder), source: this));
            _placeholder_LabelExtended.SetBinding(LabelExtended.TextColorProperty, new Binding(nameof(PlaceholderColor), source: this));
            _placeholder_LabelExtended.SetBinding(LabelExtended.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
            _placeholder_LabelExtended.SetBinding(LabelExtended.TranslationYProperty, new Binding(nameof(PlaceholderYOffset), source: this));
            _placeholder_LabelExtended.SetBinding(LabelExtended.FontSizeProperty, new Binding(nameof(FontSize), source: this));

            _placeholder_LabelExtended.TranslationX = PLACEHOLDER_DEFAULT_X_OFFSET;
            _placeholder_LabelExtended.TranslationY = PLACEHOLDER_DEFAULT_Y_OFFSET;
        }

        public double FontSize {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public double PlaceholderYOffset {
            get => (double)GetValue(PlaceholderYOffsetProperty);
            set => SetValue(PlaceholderYOffsetProperty, value);
        }

        public string FontFamily {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public Color PlaceholderColor {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        public Color TextColor {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public float LetterSpacing {
            get { return (float)GetValue(LetterSpacingProperty); }
            set { SetValue(LetterSpacingProperty, value); }
        }

        public string Placeholder {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public string InputText {
            get => (string)GetValue(InputTextProperty);
            set => SetValue(InputTextProperty, value);
        }

        private void ResolvePlaceholderVisibility() => _placeholder_LabelExtended.TranslationX = string.IsNullOrEmpty(InputText) ? PLACEHOLDER_DEFAULT_X_OFFSET : long.MaxValue;

        private void OnMainInputEditorExtendedFocused(object sender, FocusEventArgs e) => ResolvePlaceholderVisibility();

        private void OnMainInputEditorExtendedUnfocused(object sender, FocusEventArgs e) => ResolvePlaceholderVisibility();
    }
}