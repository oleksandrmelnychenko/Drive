using Drive.Client.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace Drive.Client.Services.Navigation {
    public interface INavigationService {

        ViewModelBase LastPageViewModel { get; }

        ViewModelBase PreviousPageViewModel { get; }

        Task InitializeAsync(bool canLogin);

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();

        Task GoBackAsync();
    }
}
