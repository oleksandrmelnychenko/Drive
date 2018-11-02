using System.Windows.Input;

namespace Drive.Client.ViewModels.Base {
    public interface IActionBottomBarTab {

        ICommand TabActionCommand { get; }
    }
}
