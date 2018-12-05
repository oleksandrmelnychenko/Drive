using System;
using Xamarin.Forms;

namespace Drive.Client.Controls.Stacklist.Base {
    public abstract class SourceItemBase : ContentView {

        private static readonly Color DEFAULT_SELECTED_COLOR = Color.LightGray;
        private static readonly Color DEFAULT_DESELECTED_COLOR = Color.Transparent;

        public SourceItemBase() {
            ItemSelectCommand = new Command(() => {
                SelectionAction(this);
            });

            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnTapGestureRecognizerTapped;
            GestureRecognizers.Add(tapGestureRecognizer);
        }

        public Command ItemSelectCommand { get; private set; }

        public Action<SourceItemBase> SelectionAction { get; set; }

        public bool IsSelectable { get; set; } = false;

        public bool IsOnSelectionVisualChangesEnabled { get; set; } = false;

        public Color SelectedColor { get; set; } = DEFAULT_SELECTED_COLOR;

        public Color DeselectedColor { get; set; } = DEFAULT_DESELECTED_COLOR;

        public abstract void Selected();

        public abstract void Deselected();

        private void OnTapGestureRecognizerTapped(object sender, EventArgs e) {
            if (IsSelectable && SelectionAction != null) {
                SelectionAction(this);
            }
        }
    }
}