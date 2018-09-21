using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.BottomTabViews {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchView : ContentView {
        public SearchView() {
            InitializeComponent();
        }

        private void EntryExtended_Completed(object sender, EventArgs e) {
            Debugger.Break();
        }

        //private void _listResults_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
        //    if (e.PropertyName == "ItemsSource") {
        //        if (_listResults.ItemsSource is IEnumerable<object> source && source.Any()) {
        //            Grid.SetRow(_listResults, 0);
        //        } else {
        //            Grid.SetRow(_listResults, 1);
        //        }
        //    }
        //}
    }
}