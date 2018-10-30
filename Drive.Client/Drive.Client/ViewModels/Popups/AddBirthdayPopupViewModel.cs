using Drive.Client.Helpers;
using Drive.Client.Helpers.Localize;
using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.Resources.Resx;
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

        string _day;
        public string Day {
            get { return _day; }
            set { SetProperty(ref _day, value); }
        }

        string _mounth;
        public string Mounth {
            get { return _mounth; }
            set { SetProperty(ref _mounth, value); }
        }

        string _year;
        public string Year {
            get { return _year; }
            set { SetProperty(ref _year, value); }
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

        bool _isWarningMessageVisible = false;
        public bool IsWarningMessageVisible {
            get => _isWarningMessageVisible;
            private set => SetProperty(ref _isWarningMessageVisible, value);
        }

        public ICommand DoneCommand => new Command(() => {

            if (string.IsNullOrEmpty(Day) && string.IsNullOrEmpty(Mounth) && string.IsNullOrEmpty(Year)) {
                ClosePopupCommand.Execute(null);
                BaseSingleton<GlobalSetting>.Instance.AppMessagingEvents.VehicleEvents.OnSendInformation();
            } else {
                int day = 0;
                int month = 0;
                int year = 0;

                try {
                    if (int.TryParse(Day, out day) && int.TryParse(Mounth, out month) && int.TryParse(Year, out year)) {
                        string date = string.Format("{0:D2}/{1:D2}/{2:D4}", day, month, year);

                        if (ValidationHelper.IsValidDate(date)) {
                            _searchByPersonArgs.DateOfBirth = date;
                            ClosePopupCommand.Execute(null);
                            BaseSingleton<GlobalSetting>.Instance.AppMessagingEvents.VehicleEvents.OnSendInformation();

                            IsWarningMessageVisible = false;
                        } else {
                            throw new InvalidOperationException();
                        }
                    } else {
                        throw new InvalidOperationException();
                    }
                }
                catch {
                    IsWarningMessageVisible = true;
                }
            }
            //_searchByPersonArgs.DateOfBirth = DateInput;


            //string tt = string.Format("{0:D3}", Day);
            //string h = string.Format("number are:{0:D3} - {1:D3} and {2:D4}", Day, 3, 4);

            //_searchByPersonArgs.DateOfBirth = $"{Day:D2}/{Mounth}/{Year}";

            //var cc = ValidationHelper.IsValidDate(_searchByPersonArgs.DateOfBirth);
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

        protected override void OnIsPopupVisible() {
            base.OnIsPopupVisible();

            Day = "";
            Mounth = "";
            Year = "";
            IsWarningMessageVisible = false;
        }
    }
}
