using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.ActionBars {
    internal sealed class NewPostActionBarViewModel : CommonActionBarViewModel {

        public ICommand PublishCommand => new Command(async() => await DialogService.ToastAsync("PublishCommand in developing"));
    }
}
