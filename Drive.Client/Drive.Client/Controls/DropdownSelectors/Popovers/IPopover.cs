using System.Collections;

namespace Drive.Client.Controls.DropdownSelectors.Popovers {
    public interface IPopover {

        bool IsPopoverVisible { get; set; }

        IList ItemContext { get; set; }

        object SelectedItem { get; set; }

        string HintText { get; set; }

        bool IsHaveSameWidth { get; set; }
    }
}
