using Drive.Client.Helpers;
using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.Services.Vehicle;
using Drive.Client.Validations;
using Drive.Client.Validations.ValidationRules;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.BottomTabViewModels.Bookmark;
using Drive.Client.ViewModels.IdentityAccounting;
using Drive.Client.ViewModels.Popups;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.Search {
    public sealed class SearchByPersonFinallyStepViewModel : StepBaseViewModel {

        private CancellationTokenSource _vehicleDetailsCancellationTokenSource = new CancellationTokenSource();

        private readonly IVehicleService _vehicleService;

        private SearchByPersonArgs _searchByPersonArgs;

        RequestInfoPopupViewModel _requestInfoPopupViewModel;
        public RequestInfoPopupViewModel RequestInfoPopupViewModel {
            get => _requestInfoPopupViewModel;
            private set {
                _requestInfoPopupViewModel?.Dispose();
                SetProperty(ref _requestInfoPopupViewModel, value);
            }
        }

        AddBirthdayPopupViewModel _addBirthdayPopupViewModel;
        public AddBirthdayPopupViewModel AddBirthdayPopupViewModel {
            get => _addBirthdayPopupViewModel;
            private set {
                _addBirthdayPopupViewModel?.Dispose();
                SetProperty(ref _addBirthdayPopupViewModel, value);
            }
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public SearchByPersonFinallyStepViewModel(IVehicleService vehicleService) {
            _vehicleService = vehicleService;

            StepTitle = MIDDLENAME_STEP_REGISTRATION_TITLE;
            MainInputPlaceholder = MIDDLENAME_PLACEHOLDER_STEP_REGISTRATION;
            MainInputIconPath = NAME_ICON_PATH;

            RequestInfoPopupViewModel = DependencyLocator.Resolve<RequestInfoPopupViewModel>();
            RequestInfoPopupViewModel.InitializeAsync(this);

            AddBirthdayPopupViewModel = DependencyLocator.Resolve<AddBirthdayPopupViewModel>();
            AddBirthdayPopupViewModel.InitializeAsync(this);
        }

        public override void Dispose() {
            base.Dispose();


        }

        protected override void ResetValidationObjects() {
            base.ResetValidationObjects();

            MainInput.Validations.Add(new FullNameMinLengthRule<string>() { ValidationMessage = ValidatableObject<string>.ERROR_MINLENGTH });
            MainInput.Validations.Add(new FullNameMaxLengthRule<string>() { ValidationMessage = ValidatableObject<string>.ERROR_MAXLENGTH });
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is SearchByPersonArgs searchByPersonArgs) {
                _searchByPersonArgs = searchByPersonArgs;
            }

            return base.InitializeAsync(navigationData);
        }

        protected override void OnSubscribeOnAppEvents() {
            base.OnSubscribeOnAppEvents();

            BaseSingleton<GlobalSetting>.Instance.AppMessagingEvents.LanguageEvents.TestEve += LanguageEvents_TestEve;
        }

        protected override void OnUnsubscribeFromAppEvents() {
            base.OnUnsubscribeFromAppEvents();

            BaseSingleton<GlobalSetting>.Instance.AppMessagingEvents.LanguageEvents.TestEve -= LanguageEvents_TestEve;
        }

        private void LanguageEvents_TestEve(object sender, Helpers.AppEvents.Events.Args.TestArgs e) {
            RequestInfoPopupViewModel.ShowPopupCommand.Execute(null);
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                ResetCancellationTokenSource(ref _vehicleDetailsCancellationTokenSource);
                CancellationTokenSource cancellationTokenSource = _vehicleDetailsCancellationTokenSource;

                Guid busyKey = Guid.NewGuid();
                SetBusy(busyKey, true);

                if (_searchByPersonArgs != null) {
                    try {
                        _searchByPersonArgs.MiddleName = MainInput.Value;

                        await AddBirthdayPopupViewModel.InitializeAsync(_searchByPersonArgs);
                        AddBirthdayPopupViewModel.ShowPopupCommand.Execute(null);

                        //VehicleDetailsByResidentFullName vehicleDetailsByResidentFullName = await _vehicleService.GetVehicleDetailsByResidentFullNameAsync(_searchByPersonArgs, cancellationTokenSource.Token);

                        //if (vehicleDetailsByResidentFullName != null) {
                        //    RequestInfoPopupViewModel.ShowPopupCommand.Execute(null);
                        //}
                    }
                    catch (Exception ex) {
                        Debug.WriteLine($"ERROR:{ex.Message}");
                        Debugger.Break();
                    }
                } else {
                    Debugger.Break();
                    await NavigationService.GoBackAsync();
                }
                SetBusy(busyKey, false);
            }
        }
    }
}
