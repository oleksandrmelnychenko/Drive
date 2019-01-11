using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Drive.Client.Extensions;
using Drive.Client.Helpers.Localize;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.Models.EntityModels;
using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.Resources.Resx;
using Drive.Client.Services.Automobile;
using Drive.Client.Validations;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews.Search;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels.Search {
    public class SearchByCarIdViewModel : NestedViewModel, IVisualFiguring, ISwitchTab {

        private readonly IDriveAutoService _driveAutoService;

        private CancellationTokenSource _getCarsCancellationTokenSource = new CancellationTokenSource();

        private CancellationTokenSource _getAllDriveAutoCancellationTokenSource = new CancellationTokenSource();

        public Type RelativeViewType => typeof(SearchByCarIdView);

        StringResource _tabHeader;
        public StringResource TabHeader {
            get => _tabHeader;
            private set => SetProperty(ref _tabHeader, value);
        }

        ValidatableObject<string> _validationTargetValue;
        public ValidatableObject<string> ValidationTargetValue {
            get { return _validationTargetValue; }
            set { SetProperty(ref _validationTargetValue, value); }
        }

        string _targetValue;
        public string TargetValue {
            get { return _targetValue; }
            set { SetProperty(ref _targetValue, value); }
        }

        string _errorMessage;
        public string ErrorMessage {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        private bool _hasError = false;
        public bool HasError {
            get { return _hasError; }
            set {
                SetProperty(ref _hasError, value);
                LocationVisibility = !value;
            }
        }

        DriveAutoSearch _resultSelected;
        public DriveAutoSearch ResultSelected {
            get { return _resultSelected; }
            set {
                SetProperty(ref _resultSelected, value);
                OnResulSelected(value);
            }
        }

        bool _locationVisibility = false;
        public bool LocationVisibility {
            get { return _locationVisibility; }
            set { SetProperty(ref _locationVisibility, value); }
        }

        string _locationInfo;
        public string LocationInfo {
            get { return _locationInfo; }
            set { SetProperty(ref _locationInfo, value); }
        }

        ObservableCollection<DriveAutoSearch> _foundResult;
        public ObservableCollection<DriveAutoSearch> FoundResult {
            get { return _foundResult; }
            set { SetProperty(ref _foundResult, value); }
        }

        public ICommand CleanSearchResultCommand => new Command(() => ClearSource());

        public ICommand InputCompleteCommand => new Command(async () => await OnInputCompleteAsync());

        public ICommand InputTextChangedCommand => new Command(() => OnInputTextChanged());

        /// <summary>
        ///  ctor().
        /// </summary>
        /// <param name="driveAutoService"></param>
        public SearchByCarIdViewModel(IDriveAutoService driveAutoService) {
            _driveAutoService = driveAutoService;

            _validationTargetValue = new ValidatableObject<string>();

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
                      .Finally(() => Debug.WriteLine($"---ERROR(Observable.Finally): "))
                      .Retry()
                      .Subscribe(foundResult => {
                          ApplySearchResults(foundResult);
                      });
            }
            catch (Exception ex) {
                Debug.WriteLine($"---ERROR: {ex.Message}");
            }

            SetupValidations();
        }

        public void ClearAfterTabTap() {
            ClearSource();
        }

        protected override void ResolveStringResources() {
            base.ResolveStringResources();

            TabHeader = ResourceLoader.GetString(nameof(AppStrings.ByUkraineNumberUpperCase));
        }

        private void ClearSource() {
            try {
                ValidationTargetValue.Value = string.Empty;
                FoundResult?.Clear();
                LocationInfo = string.Empty;
                TargetValue = string.Empty;
                HasError = false;
                ErrorMessage = string.Empty;
            }
            catch (Exception) {
            }
        }

        public void TabClicked() { }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is SelectedBottomBarTabArgs) {
            }

            ClearSource();

            return base.InitializeAsync(navigationData);
        }

        public override void Dispose() {
            base.Dispose();

            ResetCancellationTokenSource(ref _getCarsCancellationTokenSource);
            ResetCancellationTokenSource(ref _getAllDriveAutoCancellationTokenSource);
        }

        private void OnInputTextChanged() {
            ErrorMessage = string.Empty;
            HasError = false;
        }

        private async Task OnInputCompleteAsync() {
            FoundResult?.Clear();

            var foundCars = await GetAllDriveAutoByNumber(TargetValue);

            if (foundCars != null) {
                await NavigationService.NavigateToAsync<FoundDriveAutoViewModel>(new GetAllArg { FoundCars = foundCars.ToObservableCollection() });
            }
        }

        private async Task<IEnumerable<DriveAutoSearch>> SearchInfoAsync(string value) {
            ResetCancellationTokenSource(ref _getCarsCancellationTokenSource);
            CancellationTokenSource cancellationTokenSource = _getCarsCancellationTokenSource;

            IEnumerable<DriveAutoSearch> carInfos = null;

            if (!string.IsNullOrEmpty(value)) {
                try {
                    carInfos = await _driveAutoService.GetCarNumbersAutocompleteAsync(value, cancellationTokenSource.Token);
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR: {ex.Message}");
                    Debugger.Break();

                    ErrorMessage = ex.Message;
                    HasError = !string.IsNullOrEmpty(ErrorMessage);
                    carInfos = new DriveAutoSearch[] { };
                }
            } else {
                carInfos = new DriveAutoSearch[] { };
            }

            return carInfos;
        }

        private async Task<IEnumerable<DriveAuto>> GetAllDriveAutoByNumber(string targetCarNumber) {
            ResetCancellationTokenSource(ref _getAllDriveAutoCancellationTokenSource);
            CancellationTokenSource cancellationTokenSource = _getAllDriveAutoCancellationTokenSource;

            IEnumerable<DriveAuto> result = null;

            Guid busyKey = Guid.NewGuid();
            UpdateBusyVisualState(busyKey, true);

            try {
                result = await _driveAutoService.GetAllDriveAutosAsync(targetCarNumber, cancellationTokenSource.Token);
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR: {ex.Message}");
                ErrorMessage = ex.Message;
                HasError = true;
            }

            UpdateBusyVisualState(busyKey, false);

            return result;
        }

        private bool ValidateForm(string value) {
            ValidationTargetValue.Value = value;

            ValidationTargetValue.Validate();

            bool validationResult = ValidationTargetValue.IsValid;

            return validationResult;
        }

        private void SetupValidations() {

        }

        private void ApplySearchResults(IEnumerable<DriveAutoSearch> foundResult) {
            try {
                FoundResult = foundResult.ToObservableCollection();
                if (foundResult.Any()) {
                    LocationInfo = foundResult.FirstOrDefault().Location;
                }
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR: {ex.Message}");
                Debugger.Break();
            }
        }

        private async void OnResulSelected(DriveAutoSearch value) {
            if (value != null) {
                await NavigationService.NavigateToAsync<FoundDriveAutoViewModel>(value.Number);
                ResultSelected = null;
            }
        }
    }
}
