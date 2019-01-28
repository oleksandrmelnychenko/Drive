using Drive.Client.Controls.Stacklist;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.BottomTabViews.Calculator {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EngineTypeStackItem : CommonStackListItem {

        private static readonly Color SELECTED_CONTENT_BORDER_COLOR = (Color)App.Current.Resources["DarkBlueColor"];
        private static readonly Color SELECTED_RADIO_INDICATOR_BORDER_COLOR = (Color)App.Current.Resources["DarkBlueColor"];
        private static readonly Color SELECTED_RADIO_INDICATOR_BACKGROUND_COLOR = (Color)App.Current.Resources["DarkBlueColor"];

        private static readonly Color UNSELECTED_CONTENT_BORDER_COLOR = (Color)App.Current.Resources["LightGrayColor"];
        private static readonly Color UNSELECTED_RADIO_INDICATOR_BORDER_COLOR = (Color)App.Current.Resources["LightGrayColor"];
        private static readonly Color UNSELECTED_RADIO_INDICATOR_BACKGROUND_COLOR = (Color)App.Current.Resources["WhiteColor"];

        public EngineTypeStackItem() {
            InitializeComponent();

            Deselected();
        }

        public override void Selected() {
            _contentBorderedContainer_ExtendedContentView.BorderColor = SELECTED_CONTENT_BORDER_COLOR;
            _radioInficator_ExtendedContentView.BackgroundColor = SELECTED_RADIO_INDICATOR_BACKGROUND_COLOR;
            _radioInficator_ExtendedContentView.BorderColor = SELECTED_RADIO_INDICATOR_BORDER_COLOR;
        }

        public override void Deselected() {
            _contentBorderedContainer_ExtendedContentView.BorderColor = UNSELECTED_CONTENT_BORDER_COLOR;
            _radioInficator_ExtendedContentView.BackgroundColor = UNSELECTED_RADIO_INDICATOR_BACKGROUND_COLOR;
            _radioInficator_ExtendedContentView.BorderColor = UNSELECTED_RADIO_INDICATOR_BORDER_COLOR;
        }
    }
}