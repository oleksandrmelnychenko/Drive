using Drive.Client.Models.EntityModels;
using Drive.Client.Services.CarsInfo;
using Drive.Client.Services.RequestProvider;
using Drive.Client.ViewModels.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels {
    public sealed class MainViewModel : ViewModelBase {

        private readonly ICarInfoService _carInfoService;

        string _targetValue;
        public string TargetValue {
            get { return _targetValue; }
            set { SetProperty(ref _targetValue, value); }
        }

        IEnumerable<CarInfo> _foundResult;
        public IEnumerable<CarInfo> FoundResult {
            get { return _foundResult; }
            set { SetProperty(ref _foundResult, value); }
        }

        public ICommand TestCommand => new Command(async () => await SearchInfoAsync(TargetValue));

        /// <summary>
        ///     ctor().
        /// </summary>
        public MainViewModel(ICarInfoService carInfoService) {
            _carInfoService = carInfoService;

            try {
                //  Reactive search.
                Observable.FromEventPattern<PropertyChangedEventArgs>(this, nameof(PropertyChanged))
                    .Where(x => x.EventArgs.PropertyName == nameof(TargetValue))
                    .Throttle(TimeSpan.FromMilliseconds(700))
                    .Select(handler => Observable.FromAsync(async cancellationToken => {
                        var result = await SearchInfoAsync(TargetValue).ConfigureAwait(false);

                        if (cancellationToken.IsCancellationRequested) {
                            return new CarInfo[] { };
                        }

                        return result;
                    }))
                    .Switch()
                    .Subscribe(foundResult => {
                        ApplySearchResults(foundResult);
                    });
            }
            catch (Exception ex) {
                Debug.WriteLine($"---ERROR: {ex.Message}");
            }
        }

        private void ApplySearchResults(IEnumerable<CarInfo> foundResult) {
            FoundResult = foundResult;
        }

        public async Task<IEnumerable<CarInfo>> SearchInfoAsync(string value) {
            IEnumerable<CarInfo> carInfos = null;

            if (!string.IsNullOrEmpty(value)) {
                try {
                    carInfos = await _carInfoService.GetCarsInfoByCarIdAsync(value);
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR: {ex.Message}");
                    Debugger.Break();
                }
            } else {
                carInfos = new CarInfo[] { };
            }

            return carInfos;
        }

        public override Task InitializeAsync(object navigationData) {


            return base.InitializeAsync(navigationData);
        }
    }
}
