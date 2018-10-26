using Drive.Client.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Drive.Client.Behaviors {
    public class NumericEntryBehavior : Behavior<EntryExtended> {
        protected override void OnAttachedTo(EntryExtended bindable) {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += TextChangedHandler;
        }

        private void TextChangedHandler(object sender, TextChangedEventArgs e) {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue)) {
                bool isValid = e.NewTextValue.ToCharArray().All(x => char.IsDigit(x)); //Make sure all characters are numbers

                ((Entry)sender).Text = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }

        protected override void OnDetachingFrom(EntryExtended bindable) {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= TextChangedHandler;
        }
    }
}
