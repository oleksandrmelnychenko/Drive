using Drive.Client.Helpers.Localize;
using Drive.Client.Services.Dialog;
using Drive.Client.Services.Navigation;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.Base {
    public abstract class ViewModelBase : ExtendedBindableObject {

        private static readonly string SOURCE_URL = "https://data.gov.ua/";

        protected readonly IDialogService DialogService;

        protected readonly INavigationService NavigationService;

        /// <summary>
        ///     ctor().
        /// </summary>
        public ViewModelBase() {
            DialogService = DependencyLocator.Resolve<IDialogService>();
            NavigationService = DependencyLocator.Resolve<INavigationService>();

            BackCommand = new Command(async () => {
                await NavigationService.PreviousPageViewModel.InitializeAsync(null);
                await NavigationService.GoBackAsync();
            });

            ResourceLoader = new ResourceLoader();
            ResolveStringResources();
        }

        public ICommand BackCommand { get; protected set; }

        public ICommand NavigateToSourceCommand => new Command((object param) => {
            Device.OpenUri(new Uri(SOURCE_URL));
        });

        public ResourceLoader ResourceLoader { get; private set; }

        public bool IsSubscribedOnAppEvents { get; private set; }

        bool _isBusy;
        public bool IsBusy {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public virtual Task InitializeAsync(object navigationData) {
            if (!IsSubscribedOnAppEvents) {
                OnSubscribeOnAppEvents();
            }

            return Task.FromResult(false);
        }

        public virtual void Dispose() {
            ResourceLoader.Dispose();
            OnUnsubscribeFromAppEvents();
        }

        protected virtual void ResolveStringResources() { }

        protected void ResetCancellationTokenSource(ref CancellationTokenSource cancellationTokenSource) {
            cancellationTokenSource.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
        }

        protected virtual void OnSubscribeOnAppEvents() {
            IsSubscribedOnAppEvents = true;
        }

        protected virtual void OnUnsubscribeFromAppEvents() {
            IsSubscribedOnAppEvents = false;
        }
    }
}
