using Drive.Client.ViewModels.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Drive.Client.Services.Navigation {
    public interface INavigationService {

        bool IsBackButtonAvailable { get; }

        ViewModelBase LastPageViewModel { get; }

        ViewModelBase PreviousPageViewModel { get; }

        IReadOnlyCollection<ViewModelBase> CurrentViewModelsNavigationStack { get; }

        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();

        Task RemoveIntermediatePagesAsync();

        Task GoBackAsync();
    }
}
