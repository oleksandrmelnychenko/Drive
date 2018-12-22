using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Drive.Client.Helpers {
    public static class LayoutHelper {
        public static void SetLayoutPosition(this Layout layout, bool onFocus, int x = 0, int y = 0, uint length = 50) {
            if (onFocus) {
                layout.TranslateTo(x, y, length, Easing.Linear);
            } else {
                layout.TranslateTo(x, y, length, Easing.Linear);
            }
        }

        //if (Device.RuntimePlatform == Device.iOS) {
        //    this.TODO_ENTRY.Focused += (s, e) => { this.TODO_STACKLAYOUT.SetLayoutPosition(onFocus: true, y: -260); };
        //    this.TODO_ENTRY.Unfocused += (s, e) => { this.TODO_STACKLAYOUT.SetLayoutPosition(onFocus: false); };
        //}
    }
}
