using Drive.Client.Controls.Stacklist.Base;

namespace Drive.Client.Controls.Stacklist {
    public class CommonStackListItem : SourceItemBase {

        public override void Deselected() {
            base.Deselected();

            if (IsOnSelectionVisualChangesEnabled) {
                BackgroundColor = DeselectedColor;
            }
        }

        public override void Selected() {
            base.Selected();

            if (IsOnSelectionVisualChangesEnabled) {
                BackgroundColor = SelectedColor;
            }
        }
    }
}
