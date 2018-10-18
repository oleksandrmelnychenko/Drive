using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.BottomTabViews.Search {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchByCarIdView : ContentView {
        public SearchByCarIdView() {
            InitializeComponent();
        }

        private async void scroll_SizeChanged(object sender, EventArgs e) {
            if (_stackList.ItemsSource != null && _stackList.ItemsSource is IEnumerable<object> source && source.Any()) {
                await Task.Delay(10);
                await scroll.ScrollToAsync(_stackList, ScrollToPosition.End, false);
            }
        }
    }
}