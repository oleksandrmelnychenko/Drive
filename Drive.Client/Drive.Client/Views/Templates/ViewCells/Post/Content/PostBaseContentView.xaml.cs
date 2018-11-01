using Drive.Client.Views.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.Templates.ViewCells.Post.Content {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostBaseContentView : ContentView {

        public static readonly BindableProperty MainContentProperty =
            BindableProperty.Create(nameof(MainContent),
                typeof(View),
                typeof(PostBaseContentView),
                defaultValue: null,
                propertyChanged: (BindableObject bindable, object oldValue, object newValue) => {
                    if (bindable is PostBaseContentView declarer) {
                        declarer._mainContent.Content = newValue as View;
                    }
                });
        public View MainContent {
            get => (View)GetValue(MainContentProperty);
            set => SetValue(MainContentProperty, value);
        }

        /// <summary>
        ///     ctor()
        /// </summary>
        public PostBaseContentView() {
            InitializeComponent();
        }
    }
}