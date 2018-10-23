﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Drive.Client.Behaviors {
    public class MaskedBehavior : Behavior<Entry> {
        private string _mask = "";
        public string Mask {
            get => _mask;
            set {
                _mask = value;
                SetPositions();
            }
        }

        protected override void OnAttachedTo(Entry entry) {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry) {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        IDictionary<int, char> _positions;

        void SetPositions() {
            if (string.IsNullOrEmpty(Mask)) {
                _positions = null;
                return;
            }

            var list = new Dictionary<int, char>();
            for (var i = 0; i < Mask.Length; i++)
                if (Mask[i] != 'X')
                    list.Add(i, Mask[i]);

            _positions = list;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args) {
            var entry = sender as Entry;

            if (string.IsNullOrWhiteSpace(entry.Text) || _positions == null)
                return;

            string tt = args.NewTextValue.Substring(args.NewTextValue.Length - 1);
            if (!double.TryParse(tt, out double tempValue))
                ((Entry)sender).Text = args.OldTextValue;
            else
                ((Entry)sender).Text = args.NewTextValue;

            var text = entry.Text;

            if (text.Length > _mask.Length) {
                entry.Text = text.Remove(text.Length - 1);
                return;
            }

            foreach (var position in _positions)
                if (text.Length >= position.Key + 1) {
                    var value = position.Value.ToString();
                    if (text.Substring(position.Key, 1) != value)
                        text = text.Insert(position.Key, value);
                }

            if (entry.Text != text)
                entry.Text = text;
        }
    }
}
