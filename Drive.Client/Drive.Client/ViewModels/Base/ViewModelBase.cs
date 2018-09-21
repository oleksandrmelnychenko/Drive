using Drive.Client.Helpers;
using Drive.Client.Services.Dialog;
using Drive.Client.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.Base {
    public abstract class ViewModelBase : ExtendedBindableObject {

        protected readonly IDialogService DialogService;

        protected readonly INavigationService NavigationService;

        bool _isBusy;
        public bool IsBusy {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public ICommand BackCommand { get; protected set; }

        public ViewModelBase() {
            DialogService = DependencyLocator.Resolve<IDialogService>();
            NavigationService = DependencyLocator.Resolve<INavigationService>();

            BackCommand = new Command(async () => await NavigationService.GoBackAsync());
        }

        public virtual Task InitializeAsync(object navigationData) {


            return Task.FromResult(false);
        }

        public virtual void Dispose() {

        }

        protected void ResetCancellationTokenSource(ref CancellationTokenSource cancellationTokenSource) {
            cancellationTokenSource.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
        }
    }
}
