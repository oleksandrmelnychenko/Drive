using Drive.Client.Extensions;
using Drive.Client.Helpers;
using Drive.Client.Models.EntityModels;
using Drive.Client.Services.Automobile;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels {
    public sealed class SearchViewModel : NestedViewModel, IBottomBarTab {

        public bool IsBudgeVisible { get; private set; }

        public int BudgeCount { get; private set; }

        public string TabHeader { get; private set; }

        public string TabIcon { get; private set; } = IconPath.SEARCH;

        public Type RelativeViewType { get; private set; } = typeof(SearchView);

        public bool HasBackgroundItem => false;

        private readonly IDriveAutoService _carInfoService;

        string _targetValue;
        public string TargetValue {
            get { return _targetValue; }
            set { SetProperty(ref _targetValue, value); }
        }

        DriveAutoSearch _resultSelected;
        public DriveAutoSearch ResultSelected {
            get { return _resultSelected; }
            set {
                SetProperty(ref _resultSelected, value);
                OnResulSelected(value);
            }
        }

        private async void OnResulSelected(DriveAutoSearch value) {
            if (value != null) {
                await NavigationService.NavigateToAsync<FoundDriveAutoViewModel>(value.Number);
                ResultSelected = null;
            }
        }

        ObservableCollection<DriveAutoSearch> _foundResult;
        public ObservableCollection<DriveAutoSearch> FoundResult {
            get { return _foundResult; }
            set { SetProperty(ref _foundResult, value); }
        }

        public ICommand TestCommand => new Command(async () => await SearchInfoAsync(TargetValue));

        public ICommand CleanSearchResultCommand => new Command(() => FoundResult?.Clear());

        /// <summary>
        ///     ctor().
        /// </summary>
        public SearchViewModel(IDriveAutoService carInfoService) {
            _carInfoService = carInfoService;


            try {
                //  Reactive search.
                Observable.FromEventPattern<PropertyChangedEventArgs>(this, nameof(PropertyChanged))
                    .Where(x => x.EventArgs.PropertyName == nameof(TargetValue))
                    .Throttle(TimeSpan.FromMilliseconds(300))
                    .Select(handler => Observable.FromAsync(async cancellationToken => {
                        var result = await SearchInfoAsync(TargetValue).ConfigureAwait(false);

                        if (cancellationToken.IsCancellationRequested) {
                            return new DriveAutoSearch[] { };
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

        private void ApplySearchResults(IEnumerable<DriveAutoSearch> foundResult) {
            FoundResult = foundResult.ToObservableCollection();
        }

        public async Task<IEnumerable<DriveAutoSearch>> SearchInfoAsync(string value) {
            IEnumerable<DriveAutoSearch> carInfos = null;

            if (!string.IsNullOrEmpty(value)) {
                try {
                    carInfos = await _carInfoService.GetCarNumbersAutocompleteAsync(value);
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR: {ex.Message}");
                    Debugger.Break();
                }
            } else {
                carInfos = new DriveAutoSearch[] { };
            }

            return carInfos;
        }
    }
}
