using Drive.Client.Controls.DropdownSelectors.Popovers.Arguments;

namespace Drive.Client.Controls.DropdownSelectors.Popovers {
    public interface IPopoverKeeper {

        void ShowPopover(IPopover popiver, ShowPopoverArgs showPopoverArgs);

        void HidePopover(IPopover popover);

        void HideAllPopovers();
    }
}
