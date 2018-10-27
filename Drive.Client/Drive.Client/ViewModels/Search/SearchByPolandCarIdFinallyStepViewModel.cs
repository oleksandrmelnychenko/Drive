using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.Services.Vehicle;
using Drive.Client.ViewModels.IdentityAccounting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.Search {
    public sealed class SearchByPolandCarIdFinallyStepViewModel : StepBaseViewModel {

        private SearchByPolandNumberArgs _searchByPolandNumberArgs;
        private readonly IVehicleService _vehicleService;

        public ICommand InputTextChangedCommand => new Command(() => OnInputTextChenged());

        /// <summary>
        ///     ctor().
        /// </summary>
        public SearchByPolandCarIdFinallyStepViewModel(IVehicleService vehicleService) {
            _vehicleService = vehicleService;

            StepTitle = DATE_STEP_TITLE;
            //MainInputPlaceholder = MIDDLENAME_PLACEHOLDER_STEP_REGISTRATION;
            MainInputIconPath = DATE_ICON_PATH;
        }

        public override void Dispose() {
            base.Dispose();


        }

        private void OnInputTextChenged() {

        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is SearchByPolandNumberArgs searchByPolandNumberArgs) {
                _searchByPolandNumberArgs = searchByPolandNumberArgs;
            }

            return base.InitializeAsync(navigationData);
        }

        protected async override void OnStepCommand() {
            if (ValidateForm()) {
                if (_searchByPolandNumberArgs != null) {
                    try {
                        _searchByPolandNumberArgs.Date = MainInput.Value;

                        var tt = await _vehicleService.GetPolandVehicleDetails(_searchByPolandNumberArgs);
                    }
                    catch (Exception ex) {
                        Debug.WriteLine($"ERROR:{ex.Message}");
                        Debugger.Break();
                    }
                } else {
                    await NavigationService.GoBackAsync();
                }
            }
        }
    }
}
