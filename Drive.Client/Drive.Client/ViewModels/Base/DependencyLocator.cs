using Autofac;
using Drive.Client.DataItems.ProfileSettings;
using Drive.Client.Factories.Validation;
using Drive.Client.Factories.Vehicle;
using Drive.Client.Helpers.AppEvents;
using Drive.Client.Helpers.AppEvents.Events;
using Drive.Client.Services.Automobile;
using Drive.Client.Services.DeviceUtil;
using Drive.Client.Services.Dialog;
using Drive.Client.Services.Identity;
using Drive.Client.Services.Identity.IdentityUtility;
using Drive.Client.Services.Media;
using Drive.Client.Services.Navigation;
using Drive.Client.Services.RequestProvider;
using Drive.Client.Services.Vehicle;
using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.BottomTabViewModels;
using Drive.Client.ViewModels.BottomTabViewModels.Bookmark;
using Drive.Client.ViewModels.BottomTabViewModels.Popups;
using Drive.Client.ViewModels.BottomTabViewModels.Search;
using Drive.Client.ViewModels.IdentityAccounting.EditProfile;
using Drive.Client.ViewModels.IdentityAccounting.ForgotPassword;
using Drive.Client.ViewModels.IdentityAccounting.Registration;
using Drive.Client.ViewModels.IdentityAccounting;
using Drive.Client.ViewModels.Search;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
using Drive.Client.ViewModels.Popups;
using Drive.Client.Services.OpenUrl;

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

            // Events.
            builder.RegisterType<VehicleEvents>().SingleInstance();
            builder.RegisterType<LanguageEvents>().SingleInstance();
            builder.RegisterType<AppMessagingEvents>().SingleInstance();

            // View models.
            builder.RegisterType<HomeViewModel>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<PostViewModel>();
            builder.RegisterType<SavedViewModel>();
            builder.RegisterType<SearchViewModel>();
            builder.RegisterType<ProfileViewModel>();
            builder.RegisterType<BookmarkViewModel>();
            builder.RegisterType<EditEmailViewModel>();
            builder.RegisterType<UnauthorizeViewModel>();
            builder.RegisterType<EditUserNameViewModel>();
            builder.RegisterType<UserVehiclesViewModel>();
            builder.RegisterType<VehicleDetailViewModel>();
            builder.RegisterType<SearchByCarIdViewModel>();
            builder.RegisterType<SearchByPersonViewModel>();
            builder.RegisterType<FoundDriveAutoViewModel>();
            builder.RegisterType<EditPhoneNumberViewModel>();
            builder.RegisterType<CommonActionBarViewModel>();
            builder.RegisterType<DriveAutoDetailsViewModel>();
            builder.RegisterType<AddBirthdayPopupViewModel>();
            builder.RegisterType<NameRegisterStepViewModel>();
            builder.RegisterType<RequestInfoPopupViewModel>();
            builder.RegisterType<SignInPasswordStepViewModel>();
            builder.RegisterType<LanguageSelectPopupViewModel>();
            builder.RegisterType<PasswordRegisterStepViewModel>();
            builder.RegisterType<UpdateAppVersionPopupViewModel>();
            builder.RegisterType<SignInPhoneNumberStepViewModel>();
            builder.RegisterType<EditPasswordFirstStepViewModel>();
            builder.RegisterType<EditPasswordSecondStepViewModel>();
            builder.RegisterType<PhoneNumberRegisterStepViewModel>();
            builder.RegisterType<EditPasswordFinallyStepViewModel>();
            builder.RegisterType<ForgotPasswordFirstStepViewModel>();
            builder.RegisterType<SearchByPersonSecondStepViewModel>();
            builder.RegisterType<ForgotPasswordSecondStepViewModel>();
            builder.RegisterType<SearchByPersonFinallyStepViewModel>();
            builder.RegisterType<ForgotPasswordFinallyStepViewModel>();
            builder.RegisterType<IdentityAccountingActionBarViewModel>();
            builder.RegisterType<ConfirmPasswordRegisterStepViewModel>();

            // Services.
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<OpenUrlService>().As<IOpenUrlService>();
            builder.RegisterType<VehicleService>().As<IVehicleService>();
            builder.RegisterType<RequestProvider>().As<IRequestProvider>().SingleInstance();
            builder.RegisterType<IdentityService>().As<IIdentityService>();
            builder.RegisterType<DriveAutoService>().As<IDriveAutoService>();
            builder.RegisterType<PickMediaService>().As<IPickMediaService>();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<DeviceUtilService>().As<IDeviceUtilService>().SingleInstance();
            builder.RegisterType<IdentityUtilityService>().As<IIdentityUtilityService>();

            // Factories.
            builder.RegisterType<VehicleFactory>().As<IVehicleFactory>();
            builder.RegisterType<ValidationObjectFactory>().As<IValidationObjectFactory>();

            // Data items
            builder.RegisterType<ProfileSettingsDataItems>().As<IProfileSettingsDataItems>().SingleInstance();

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
            if (viewModelType == null) {
                Debug.WriteLine("------------------------ERROR: Can't find viewModel type ---------------------");
                return;
            } 

            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
