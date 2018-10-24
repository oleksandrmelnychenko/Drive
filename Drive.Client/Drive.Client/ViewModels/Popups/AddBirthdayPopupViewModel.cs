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

        public override Type RelativeViewType => typeof(AddBirthdayPopupView);

        string _dateInput;
        public string DateInput {
            get { return _dateInput; }
            set { SetProperty(ref _dateInput, value); }
        }

        SearchByPersonArgs _searchByPersonArgs;
        public SearchByPersonArgs SearchByPersonArgs {
            get { return _searchByPersonArgs; }
            set {
                SetProperty(ref _searchByPersonArgs, value);
                OnPropertyChanged("SearchByPersonArgs");
            }
        }

        string _fullName;
        public string FullName {
            get { return _fullName; }
            set { SetProperty(ref _fullName, value); }
        }

        public ICommand DoneCommand => new Command(() => {
            ClosePopupCommand.Execute(null);
            _searchByPersonArgs.DateOfBirth = DateInput;
            BaseSingleton<GlobalSetting>.Instance.AppMessagingEvents.VehicleEvents.OnSendInformation();
        });

        /// <summary>
        ///     ctor().
        /// </summary>
        public AddBirthdayPopupViewModel() {
          
        }

        public override Task InitializeAsync(object navigationData) {
            if (navigationData is SearchByPersonArgs searchByPersonArgs) {
                SearchByPersonArgs = searchByPersonArgs;
                FullName = searchByPersonArgs.FullName;
            }

            return base.InitializeAsync(navigationData);
        }
    }
}
