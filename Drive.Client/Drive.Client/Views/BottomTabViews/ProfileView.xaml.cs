using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.BottomTabViews {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileView : ContentView {

        private static readonly string _PENCIL_TRANSLATE_ANIMATION_NAME = "pencil_translate_animation_name_Drive.Client.Views.BottomTabViews.ProfileView";
        private readonly double _PENCIL_TRANSLATION_Y_LIMIT_ANIMATION_VALUE = 80;
        private readonly double _Y_SCROLL_TRANSLATION_LIMIT_VALUE = 40;
        private readonly double _Y_SCROLL_SCALE_LIMIT_VALUE = 150;
        private readonly double _SCALE_LIMIT_VALUE = .5;
        private readonly double _SCALE_MULTIPLIER = 2.5;
        private readonly double _PENCIL_DEFAULT_Y_TRANSLATION = 27;

        private bool _isHiding = false;
        private bool _isShowing = false;

        public ProfileView() {
            InitializeComponent();

            _changeAvatarPencil_SvgCachedImage.TranslationY = _PENCIL_DEFAULT_Y_TRANSLATION;
        }

        private void OnScrollViewScrolled(object sender, ScrolledEventArgs e) {
            AvatarScopeTranslation(e);
            AvatarScaleTransformation(e);
            AvatarPencilTranslation(e);
        }

        private void AvatarPencilTranslation(ScrolledEventArgs e) {
            ///
            /// iOS allowed less than zero values
            /// 
            if (e.ScrollY > 0) {
                if (e.ScrollY > 20 && !_isHiding && _changeAvatarPencil_SvgCachedImage.TranslationY != _PENCIL_TRANSLATION_Y_LIMIT_ANIMATION_VALUE) {
                    this.AbortAnimation(_PENCIL_TRANSLATE_ANIMATION_NAME);
                    _isHiding = true;
                    _changeAvatarPencil_SvgCachedImage.InputTransparent = true;
                    _avatarImage_CachedImage.InputTransparent = false;

                    new Animation((v) => _changeAvatarPencil_SvgCachedImage.TranslationY = v, start: _changeAvatarPencil_SvgCachedImage.TranslationY, end: _PENCIL_TRANSLATION_Y_LIMIT_ANIMATION_VALUE, easing: Easing.Linear)
                        .Commit(this, _PENCIL_TRANSLATE_ANIMATION_NAME, length: 125, finished: (d, b) => {
                            _changeAvatarPencil_SvgCachedImage.TranslationY = _PENCIL_TRANSLATION_Y_LIMIT_ANIMATION_VALUE;
                            _isHiding = false;
                        });
                }
                else if (e.ScrollY <= 20 && !_isShowing && _changeAvatarPencil_SvgCachedImage.TranslationY != _PENCIL_DEFAULT_Y_TRANSLATION) {
                    this.AbortAnimation(_PENCIL_TRANSLATE_ANIMATION_NAME);
                    _isShowing = true;
                    _changeAvatarPencil_SvgCachedImage.InputTransparent = false;
                    _avatarImage_CachedImage.InputTransparent = true;

                    new Animation((v) => _changeAvatarPencil_SvgCachedImage.TranslationY = v, start: _changeAvatarPencil_SvgCachedImage.TranslationY, end: _PENCIL_DEFAULT_Y_TRANSLATION,easing:Easing.Linear)
                        .Commit(this, _PENCIL_TRANSLATE_ANIMATION_NAME, length: 125, finished: (d, b) => {
                            _changeAvatarPencil_SvgCachedImage.TranslationY = _PENCIL_DEFAULT_Y_TRANSLATION;
                            _isShowing = false;
                        });
                }
            }
            else {
                _changeAvatarPencil_SvgCachedImage.TranslationY = _PENCIL_DEFAULT_Y_TRANSLATION;
                _changeAvatarPencil_SvgCachedImage.InputTransparent = false;
            }
        }

        private double AvatarScaleTransformation(ScrolledEventArgs e) {
            ///
            /// iOS allowed less than zero values
            ///
            if (e.ScrollY > 0) {
                if (_avatar_Grid.Scale > 1) {
                    _avatar_Grid.Scale = 1;
                }
                else {
                    double targetScale = 1 - ((_SCALE_LIMIT_VALUE * (e.ScrollY * _SCALE_MULTIPLIER)) / _Y_SCROLL_SCALE_LIMIT_VALUE);

                    _avatar_Grid.Scale = targetScale > _SCALE_LIMIT_VALUE ? targetScale : _SCALE_LIMIT_VALUE;
                }
            }
            else {
                _avatar_Grid.Scale = 1;
            }

            return _avatar_Grid.Scale;
        }

        private void AvatarScopeTranslation(ScrolledEventArgs e) {
            ///
            /// iOS allowed less than zero values
            ///
            if (e.ScrollY > 0) {
                if (e.ScrollY > _Y_SCROLL_TRANSLATION_LIMIT_VALUE) {
                    _avatarScope_ContentView.TranslationY = _Y_SCROLL_TRANSLATION_LIMIT_VALUE * -1;
                }
                else {
                    _avatarScope_ContentView.TranslationY = e.ScrollY * -1;
                }
            }
            else {
                _avatarScope_ContentView.TranslationY = 0;
            }
        }
    }
}