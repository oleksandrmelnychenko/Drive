using Drive.Client.Helpers;
using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.Services.Vehicle;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.Popups;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.Popups {
    public class AddBirthdayPopupViewModel : PopupBaseViewModel {

        string _dateInput;
        public string DateInput {
            get { return _dateInput; }
            set { SetProperty(ref _dateInput, value); }
        }

        private readonly IVehicleService _vehicleService;

        SearchByPersonArgs _searchByPersonArgs;
        public SearchByPersonArgs SearchByPersonArgs {
            get { return _searchByPersonArgs; }
            set { SetProperty(ref _searchByPersonArgs, value); }
        }

        public AddBirthdayPopupViewModel(IVehicleService vehicleService) {
            _vehicleService = vehicleService;
        }

        public ICommand DoneCommand => new Command(() => {
            _searchByPersonArgs.DateOfBirth = DateInput;

            Task.Run(async () => {
                VehicleDetailsByResidentFullName vehicleDetailsByResidentFullName = await _vehicleService.GetVehicleDetailsByResidentFullNameAsync(_searchByPersonArgs);

                if (vehicleDetailsByResidentFullName != null) {
                    BaseSingleton<GlobalSetting>.Instance.AppMessagingEvents.LanguageEvents.OnTestEve(vehicleDetailsByResidentFullName);
                }
            });

            ClosePopupCommand.Execute(null);
        });

        public override Type RelativeViewType => typeof(AddBirthdayPopupView);

        public override Task InitializeAsync(object navigationData) {
            if (navigationData is SearchByPersonArgs searchByPersonArgs) {
                SearchByPersonArgs = searchByPersonArgs;
            }

            return base.InitializeAsync(navigationData);
        }
    }
}
