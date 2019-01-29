using Drive.Client.Controls.Stacklist;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.BottomTabViews.Calculator {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrencyStackItem : CommonStackListItem {

        private static readonly Color SELECTED_BACKGROUND_COLOR = (Color)App.Current.Resources["WhiteColor"];
        private static readonly Color SELECTED_TEXT_COLOR = (Color)App.Current.Resources["DarkColor"];
        private static readonly Color SELECTED_BORDER_COLOR = (Color)App.Current.Resources["LightGrayColor"];
        private static readonly double SELECTED_ICON_OPACITY = 1.0;

        private static readonly Color UNSELECTED_BACKGROUND_COLOR = (Color)App.Current.Resources["ExtraLightGrayColor"];
        private static readonly Color UNSELECTED_TEXT_COLOR = (Color)App.Current.Resources["BaseGrayColor"];
        private static readonly Color UNSELECTED_BORDER_COLOR = Color.Transparent;
        private static readonly double UNSELECTED_ICON_OPACITY = .4;

        public CurrencyStackItem() {
            InitializeComponent();

            Deselected();
        }

        public override void Selected() {
            base.Selected();
            _shadow_SKCanvasView.InvalidateSurface();

            _backgroundPad_ExtendedContentView.BackgroundColor = SELECTED_BACKGROUND_COLOR;
            _backgroundPad_ExtendedContentView.BorderColor = SELECTED_BORDER_COLOR;
            _title_LabelExtended.TextColor = SELECTED_TEXT_COLOR;
            _icon_SvgCachedImage.Opacity = SELECTED_ICON_OPACITY;
        }

        public override void Deselected() {
            base.Deselected();
            _shadow_SKCanvasView.InvalidateSurface();

            _backgroundPad_ExtendedContentView.BackgroundColor = UNSELECTED_BACKGROUND_COLOR;
            _backgroundPad_ExtendedContentView.BorderColor = UNSELECTED_BORDER_COLOR;
            _title_LabelExtended.TextColor = UNSELECTED_TEXT_COLOR;
            _icon_SvgCachedImage.Opacity = UNSELECTED_ICON_OPACITY;
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args) {
            if (IsSelected) {
                SKCanvas skCanvas = args.Surface.Canvas;
                SKImageInfo skinfo = args.Info;
                skCanvas.Clear();

                float shadowOffset = 6f;
                float rectangleOffset = 12f;

                SKColor rectangleStyleFillShadowColor = new SKColor(187, 187, 187, 255);
                SKImageFilter rectangleStyleFillShadow = SKImageFilter.CreateDropShadow(0f, 0f, shadowOffset, shadowOffset, rectangleStyleFillShadowColor, SKDropShadowImageFilterShadowMode.DrawShadowOnly, null, null);

                SKPaint rectangleStyleFillPaint = new SKPaint() {
                    Style = SKPaintStyle.Fill,
                    BlendMode = SKBlendMode.SrcOver,
                    IsAntialias = true,
                    ImageFilter = rectangleStyleFillShadow
                };

                skCanvas.DrawRect(new SKRect(rectangleOffset, rectangleOffset, skinfo.Width - rectangleOffset, skinfo.Height - rectangleOffset), rectangleStyleFillPaint);
            }
            else {
                args.Surface.Canvas.Clear();
            }
        }
    }
}