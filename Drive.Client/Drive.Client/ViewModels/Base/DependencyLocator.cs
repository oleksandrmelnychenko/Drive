using Autofac;
using Drive.Client.Services.Automobile;
using Drive.Client.Services.Dialog;
using Drive.Client.Services.Navigation;
using Drive.Client.Services.RequestProvider;
using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.BottomTabViewModels;
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.Base {
    public static class DependencyLocator {

        private static IContainer _container;

        public static readonly BindableProperty AutoWireViewModelProperty =
          BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(DependencyLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable) {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value) {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }

        public static void RegisterDependencies() {
            var builder = new ContainerBuilder();

            // View models.
            builder.RegisterType<HomeViewModel>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<PostViewModel>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<SearchViewModel>();
            builder.RegisterType<ProfileViewModel>();
            builder.RegisterType<BookmarkViewModel>();
            builder.RegisterType<FoundDriveAutoViewModel>();
            builder.RegisterType<CommonActionBarViewModel>();
            builder.RegisterType<DriveAutoDetailsViewModel>();

            // Services.
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<DriveAutoService>().As<IDriveAutoService>();
            builder.RegisterType<RequestProvider>().As<IRequestProvider>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
           
            // Factories.

            if (_container != null) {
                _container.Dispose();
            }
            _container = builder.Build();
        }

        public static T Resolve<T>() => _container.Resolve<T>();

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue) {
            if (!(bindable is Element view)) return;

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null) return;

            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
