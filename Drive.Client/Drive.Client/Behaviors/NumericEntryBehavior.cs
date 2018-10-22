using Drive.Client.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Drive.Client.Behaviors {
    public class NumericEntryBehavior : Behavior<EntryExtended> {
        protected override void OnAttachedTo(EntryExtended bindable) {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += TextChangedHandler;
        }

        private void TextChangedHandler(object sender, TextChangedEventArgs e) {
            //  Short circuit for no value
            if (string.IsNullOrEmpty(e.NewTextValue))
                return;

            if (!double.TryParse(e.NewTextValue, out double tempValue))
                ((Entry)sender).Text = e.OldTextValue;
            else
                ((Entry)sender).Text = e.NewTextValue;
        }

        protected override void OnDetachingFrom(EntryExtended bindable) {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= TextChangedHandler;
        }
    }
}
