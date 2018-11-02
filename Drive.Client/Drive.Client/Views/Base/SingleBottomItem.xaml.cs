using Drive.Client.ViewModels.Base;
using FFImageLoading.Forms;
using FFImageLoading.Transformations;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.Base {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleBottomItem : ContentView {

        private static readonly ColorSpaceTransformation DEFAULT_ICON_COLOR = new ColorSpaceTransformation(Color.White.ColorToMatrix());

        private static readonly ColorSpaceTransformation SELECTED_ICON_COLOR_TRANSFORMATION =
            new ColorSpaceTransformation(((Color)App.Current.Resources["DarkBlueColor"]).ColorToMatrix());

        public BindableProperty IsSelectedProperty = BindableProperty.Create(
            nameof(IsSelected),
            typeof(bool),
            typeof(SingleBottomItem),
            defaultValue: false,
            propertyChanged: (BindableObject bindable, object oldValue, object newValue) => (bindable as SingleBottomItem).OnIsSelected(bindable));

        public SingleBottomItem() {
            InitializeComponent();

            SetBinding(IsSelectedProperty, new Binding("IsSelected", mode: BindingMode.TwoWay));

            OnIsSelected();
        }

        public View AppropriateItemContentView { get; private set; }

        public int TabIndex { get; set; }

        public bool IsSelected {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public Color _themeColor;
        public Color ThemeColor {
            get => _themeColor;
            set {
                _themeColor = value;
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if (propertyName == BindingContextProperty.PropertyName) {
                if (BindingContext is IBottomBarTab bottomBarItemContext) {
                    ApplyRelativeView(bottomBarItemContext);
                }
            }
        }

        private void OnIsSelected(BindableObject bindable = default(BindableObject)) {
            if (IsSelected) {
                if (bindable != null) {
                    if (bindable is SingleBottomItem singleBottomItem) {
                        if (singleBottomItem.BindingContext is IBottomBarTab bottomBarTab) {
                            if (bottomBarTab.HasBackgroundItem) {
                                _icon_CachedImage.Transformations.Add(DEFAULT_ICON_COLOR);
                                _icon_CachedImage.ReloadImage();
                                //_icon_CachedImage.Rotation = 45;
                            }
                            else {
                                _icon_CachedImage.Transformations.Add(SELECTED_ICON_COLOR_TRANSFORMATION);
                                _icon_CachedImage.ReloadImage();
                            }
                        }
                    }
                }
            }
            else {
                _icon_CachedImage.Transformations.Clear();
                _icon_CachedImage.ReloadImage();
                _icon_CachedImage.Rotation = 0;
            }
        }

        private void ApplyRelativeView(IBottomBarTab bottomBarItemContext) {
            if (!(bottomBarItemContext is IActionBottomBarTab)) {
                AppropriateItemContentView = (View)new DataTemplate(bottomBarItemContext.RelativeViewType).CreateContent();
                AppropriateItemContentView.BindingContext = bottomBarItemContext;
            }
        }
    }
}