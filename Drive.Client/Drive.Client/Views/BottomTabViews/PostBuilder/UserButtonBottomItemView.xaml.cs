using Drive.Client.Models.Identities;
using Drive.Client.Views.Base;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.BottomTabViews.PostBuilder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserButtonBottomItemView : BottomItemViewBase {

        private static readonly double _buttonHideYTranslation = 120;
        private static readonly uint _buttonTranslationSpeed = 70;

        public static readonly BindableProperty UserButtonStateProperty = BindableProperty.Create(
            nameof(UserButtonState),
            typeof(UserButtonState),
            typeof(UserButtonBottomItemView),
            defaultValue: default(UserButtonState),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (BindableObject bindable, object oldValue, object newValue) => { if (bindable is UserButtonBottomItemView declarer) declarer.OnUserButtonState(); });

        public UserButtonBottomItemView() {
            InitializeComponent();
            ComponentInitialized();

            SetBinding(UserButtonStateProperty, new Binding(nameof(UserButtonState)));

            _visionSearchByImageButton_ContentView.TranslationY = _buttonHideYTranslation;
            _createPostButton_SvgCachedImage.TranslationY = _buttonHideYTranslation;

            OnUserButtonState();
        }

        public UserButtonState UserButtonState {
            get => (UserButtonState)GetValue(UserButtonStateProperty);
            set => SetValue(UserButtonStateProperty, value);
        }

        private async void OnUserButtonState() {
            switch (UserButtonState) {
                case UserButtonState.CreateNewPost:
                    _visionSearchByImageButton_ContentView.InputTransparent = true;
                    await _visionSearchByImageButton_ContentView.TranslateTo(0, _buttonHideYTranslation, _buttonTranslationSpeed);

                    _createPostButton_SvgCachedImage.InputTransparent = false;
                    await _createPostButton_SvgCachedImage.TranslateTo(0, 0, _buttonTranslationSpeed);
                    break;
                case UserButtonState.SearchByImage:
                    _createPostButton_SvgCachedImage.InputTransparent = true;
                    await _createPostButton_SvgCachedImage.TranslateTo(0, _buttonHideYTranslation, _buttonTranslationSpeed);

                    _visionSearchByImageButton_ContentView.InputTransparent = false;
                    await _visionSearchByImageButton_ContentView.TranslateTo(0, 0, _buttonTranslationSpeed);
                    break;
                default:
                    Debugger.Break();
                    throw new InvalidOperationException("Unhandled UserButtonState");
            }
        }
    }
}