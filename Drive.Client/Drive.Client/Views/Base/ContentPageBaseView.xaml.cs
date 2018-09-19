using Drive.Client.Controls.ActionBars;
using Drive.Client.Controls.Popups;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.Base {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentPageBaseView : ContentPage {
        private static readonly string _BUSY_BINDING_PATH = "IsBusy";
        private static readonly string _IS_POPUP_VISIBLE_BINDING_PATH = "IsPopupsVisible";
        private static readonly string _POPUPS_BINDING_PATH = "Popups";

        private static readonly string _IS_PULL_TO_REFRESH_ENABLED_BINDING_PATH = "IsPullToRefreshEnabled";
        private static readonly string _IS_REFRESHING_BINDING_PATH = "IsRefreshing";
        private static readonly string _REFRESH_COMMAND_BINDING_PATH = "RefreshCommand";

        public static readonly BindableProperty MainContentProperty =
            BindableProperty.Create(nameof(MainContent),
                typeof(View),
                typeof(ContentPageBaseView),
                defaultValue: null,
                propertyChanged: (BindableObject bindable, object oldValue, object newValue) => {
                    if (bindable is ContentPageBaseView declarer) {
                        declarer._contentBox_Grid.Children.Add(newValue as View);
                    }
                });

        public static readonly BindableProperty ActionBarProperty =
            BindableProperty.Create(nameof(ActionBar),
                typeof(ActionBarBaseView),
                typeof(ContentPageBaseView),
                defaultValue: null,
                propertyChanged: (BindableObject bindable, object oldValue, object newValue) => {
                    if (bindable is ContentPageBaseView declarer) {
                        declarer._actionBarSpot_ContentView.Content = newValue as ActionBarBaseView;
                    }
                });

        public static readonly BindableProperty IsBusyAwaitingProperty =
            BindableProperty.Create(nameof(IsBusyAwaiting),
                typeof(bool),
                typeof(ContentPageBaseView),
                defaultValue: false,
                propertyChanged: (BindableObject bindable, object oldValue, object newValue) => {
                    if (bindable is ContentPageBaseView declarer) {
                        declarer._busyIndicator_Indicator.IsVisible = declarer.IsBusyAwaiting;

                        if (declarer.IsBusyAwaiting) {
                            Grid.SetRow(declarer._busyIndicator_Indicator, 0);
                        } else {
                            Grid.SetRow(declarer._busyIndicator_Indicator, 1);
                        }
                    }
                });

        public static readonly BindableProperty IsPopupVisibleProperty =
            BindableProperty.Create(nameof(IsPopupVisible),
                typeof(bool),
                typeof(ContentPageBaseView),
                defaultValue: false,
                propertyChanged: async (BindableObject bindable, object oldValue, object newValue) => {
                    if (bindable is ContentPageBaseView declarer) {
                        if (declarer.IsPopupVisible) {
                            declarer._popupSpot_ContentView.Opacity = 0;
                            Grid.SetRow(declarer._popupSpot_ContentView, 0);
                            await declarer._popupSpot_ContentView.FadeTo(1);
                        } else {
                            declarer._popupSpot_ContentView.Opacity = 1;
                            await declarer._popupSpot_ContentView.FadeTo(0);
                            Grid.SetRow(declarer._popupSpot_ContentView, 1);

                            declarer._popupsKeeper_PopupsBlockView.CloseAllPopups();
                        }
                    }
                });

        public static readonly BindableProperty IsPullToRefreshEnabledProperty =
            BindableProperty.Create(nameof(IsPullToRefreshEnabled),
                typeof(bool),
                typeof(ContentPageBaseView),
                defaultValue: default(bool));

        public static readonly BindableProperty IsRefreshingProperty =
            BindableProperty.Create(nameof(IsRefreshing),
                typeof(bool),
                typeof(ContentPageBaseView),
                defaultValue: default(bool));

        public static readonly BindableProperty RefreshCommandProperty =
            BindableProperty.Create(nameof(RefreshCommand),
                typeof(ICommand),
                typeof(ContentPageBaseView));

        private TapGestureRecognizer _popupBlockBackingTapGesture = new TapGestureRecognizer();

        public ContentPageBaseView() {
            InitializeComponent();

            _popupBlockBackingTapGesture.Command = new Command(() => {
                IsPopupVisible = false;
            });

            SetBinding(IsBusyAwaitingProperty, new Binding(_BUSY_BINDING_PATH));
            SetBinding(IsPopupVisibleProperty, new Binding(_IS_POPUP_VISIBLE_BINDING_PATH, BindingMode.TwoWay));
            SetBinding(IsPullToRefreshEnabledProperty, new Binding(_IS_PULL_TO_REFRESH_ENABLED_BINDING_PATH, BindingMode.OneWay));
            SetBinding(IsRefreshingProperty, new Binding(_IS_REFRESHING_BINDING_PATH, BindingMode.TwoWay));
            SetBinding(RefreshCommandProperty, new Binding(_REFRESH_COMMAND_BINDING_PATH, BindingMode.OneWay));

            //_mainContentSpot_PullToRefreshLayout.SetBinding(PullToRefreshLayout.IsPullToRefreshEnabledProperty, new Binding(_IS_PULL_TO_REFRESH_ENABLED_BINDING_PATH, mode: BindingMode.OneWay, source: this));
            //_mainContentSpot_PullToRefreshLayout.SetBinding(PullToRefreshLayout.IsRefreshingProperty, new Binding(_IS_REFRESHING_BINDING_PATH, mode: BindingMode.TwoWay, source: this));
            //_mainContentSpot_PullToRefreshLayout.SetBinding(PullToRefreshLayout.RefreshCommandProperty, new Binding(_REFRESH_COMMAND_BINDING_PATH, mode: BindingMode.OneWay, source: this));

            _popupsKeeper_PopupsBlockView.SetBinding(PopupsBlockView.PopupsProperty, new Binding(_POPUPS_BINDING_PATH, mode: BindingMode.OneWay));
        }

        public ICommand RefreshCommand {
            get { return (ICommand)GetValue(RefreshCommandProperty); }
            set { SetValue(RefreshCommandProperty, value); }
        }


        public bool IsRefreshing {
            get => (bool)GetValue(IsRefreshingProperty);
            set => SetValue(IsRefreshingProperty, value);
        }

        public bool IsPullToRefreshEnabled {
            get => (bool)GetValue(IsPullToRefreshEnabledProperty);
            set => SetValue(IsPullToRefreshEnabledProperty, value);
        }

        public bool IsPopupVisible {
            get => (bool)GetValue(IsPopupVisibleProperty);
            set => SetValue(IsPopupVisibleProperty, value);
        }

        public View MainContent {
            get => (View)GetValue(MainContentProperty);
            set => SetValue(MainContentProperty, value);
        }

        public ActionBarBaseView ActionBar {
            get => (ActionBarBaseView)GetValue(ActionBarProperty);
            set => SetValue(ActionBarProperty, value);
        }

        public bool IsBusyAwaiting {
            get => (bool)GetValue(IsBusyAwaitingProperty);
            set => SetValue(IsBusyAwaitingProperty, value);
        }
    }
}