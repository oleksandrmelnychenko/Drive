using Drive.Client.ViewModels;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Drive.Client.Services.Navigation {
    public class NavigationService : INavigationService {

        public ViewModelBase PreviousPageViewModel {
            get {
                var mainPage = Application.Current.MainPage as CustomNavigationView;
                var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as ViewModelBase;
            }
        }

        public ViewModelBase LastPageViewModel {
            get {
                if (Application.Current.MainPage is CustomNavigationView) {
                    return ((CustomNavigationView)Application.Current.MainPage).Navigation.NavigationStack.LastOrDefault().BindingContext as ViewModelBase;
                }

                return null;
            }
        }

        public Task InitializeAsync(bool canLogin) {
            if (canLogin) {
                return NavigateToAsync<LoginViewModel>();
            } else
                return NavigateToAsync<MainViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase =>
             InternalNavigateToAsync(typeof(TViewModel), null);

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase =>
             InternalNavigateToAsync(typeof(TViewModel), parameter);

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter) {
            if (Application.Current.MainPage is CustomNavigationView navigationPage) {
                List<Page> stack = navigationPage.Navigation.NavigationStack.ToList();
                Page targetPage = stack.FirstOrDefault(p => {
                    return p.BindingContext.GetType().FullName == viewModelType.FullName;
                });

                if (targetPage != null) {
                    int targetPageIndex = stack.IndexOf(targetPage);
                    int stepsToForBackStack = 0;

                    for (int i = 0; i < stack.Count; i++) {
                        if (i > targetPageIndex) {
                            stepsToForBackStack++;
                        }
                    }

                    if (stepsToForBackStack > 1) {
                        stepsToForBackStack--;

                        for (int i = 1; i <= stepsToForBackStack; i++) {
                            await RemoveLastFromBackStackAsync();
                        }

                        await GoBackAsync();
                    } else if (stepsToForBackStack == 1) {
                        await GoBackAsync();
                    }

                    await ((ViewModelBase)targetPage.BindingContext).InitializeAsync(parameter);
                } else {
                    Page page = CreatePage(viewModelType, parameter);
                    await navigationPage.PushAsync(page, false);
                    await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
                }
            } else {
                Page page = CreatePage(viewModelType, parameter);
                Application.Current.MainPage = new CustomNavigationView(page);
                await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
            }
        }

        public Task RemoveLastFromBackStackAsync() {
            if (Application.Current.MainPage is CustomNavigationView mainPage) {
                Page pageToRemove = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2];
                DisposeBindingContext(pageToRemove);
                mainPage.Navigation.RemovePage(pageToRemove);
            }

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync() {
            if (Application.Current.MainPage is CustomNavigationView mainPage) {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++) {
                    var page = mainPage.Navigation.NavigationStack[i];
                    DisposeBindingContext(page);
                    mainPage.Navigation.RemovePage(page);
                }
            }
            return Task.FromResult(true);
        }

        public async Task GoBackAsync() {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            DisposeBindingContext(mainPage.CurrentPage);

            await mainPage.PopAsync();
        }

        private Page CreatePage(Type viewModelType, object parameter) {
            try {
                Type pageType = GetPageTypeForViewModel(viewModelType);
                if (pageType == null) {
                    throw new Exception($"Cannot locate page type for {viewModelType}");
                }

                return Activator.CreateInstance(pageType) as Page;
            }
            catch (Exception) {
                Debugger.Break();
                throw;
            }
        }

        private Type GetPageTypeForViewModel(Type viewModelType) {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }


        private void DisposeBindingContext(Page targetPage) {
            if (targetPage?.BindingContext is ViewModelBase) {
                ((ViewModelBase)targetPage.BindingContext).Dispose();
            }
        }
    }
}
