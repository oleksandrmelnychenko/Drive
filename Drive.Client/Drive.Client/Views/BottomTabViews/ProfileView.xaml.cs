using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.BottomTabViews {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileView : ContentView {

        private readonly double _Y_SCROLL_TRANSLATION_LIMIT_VALUE = 40;
        private readonly double _Y_SCROLL_SCALE_LIMIT_VALUE = 150;
        private readonly double _SCALE_LIMIT_VALUE = .6;
        private readonly double _SCALE_MULTIPLIER = 2.5;

        public ProfileView() {
            InitializeComponent();
        }

        private void OnScrollViewScrolled(object sender, ScrolledEventArgs e) {
            AvatarScopeTranslation(e);
            AvatarScaleTransformation(e);
        }

        private double AvatarScaleTransformation(ScrolledEventArgs e) {
            if (_avatar_Grid.Scale > 1) {
                _avatar_Grid.Scale = 1;
            }
            else {
                double targetScale = 1 - ((_SCALE_LIMIT_VALUE * (e.ScrollY * _SCALE_MULTIPLIER)) / _Y_SCROLL_SCALE_LIMIT_VALUE);

                _avatar_Grid.Scale = targetScale > _SCALE_LIMIT_VALUE ? targetScale : _SCALE_LIMIT_VALUE;
            }

            return _avatar_Grid.Scale;
        }

        private void AvatarScopeTranslation(ScrolledEventArgs e) {
            if (e.ScrollY > _Y_SCROLL_TRANSLATION_LIMIT_VALUE) {
                _avatarScope_ContentView.TranslationY = _Y_SCROLL_TRANSLATION_LIMIT_VALUE * -1;
            }
            else {
                _avatarScope_ContentView.TranslationY = e.ScrollY * -1;
            }
        }
    }
}