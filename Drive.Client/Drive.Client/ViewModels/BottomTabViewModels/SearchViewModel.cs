using Drive.Client.Extensions;
using Drive.Client.Helpers;
using Drive.Client.Models.EntityModels;
using Drive.Client.Models.EntityModels.Identity;
using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.Services.Automobile;
using Drive.Client.Validations;
using Drive.Client.Validations.ValidationRules;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels {
    public sealed class SearchViewModel : NestedViewModel, IBottomBarTab {

        private CancellationTokenSource _getCarsCancellationTokenSource = new CancellationTokenSource();

        private readonly IDriveAutoService _driveAutoService;

        public bool IsBudgeVisible { get; private set; }

        public int BudgeCount { get; private set; }

        public string TabHeader { get; private set; }

        public string TabIcon { get; private set; } = IconPath.SEARCH;

        public Type RelativeViewType { get; private set; } = typeof(SearchView);

        public bool HasBackgroundItem => false;

        public ICommand CleanSearchResultCommand => new Command(() => ClearFoundedResult());

        public ICommand InputCompleteCommand => new Command(async () => {
            FoundResult?.Clear();

          

            var foundCars = await GetAllDriveAutoByNumber(TargetValue);

           

            if (foundCars != null) {
                await NavigationService.NavigateToAsync<FoundDriveAutoViewModel>(new GetAllArg { FoundCars = foundCars.ToObservableCollection() });
            }
        });

        public ICommand InputTextChangedCommand => new Command(() => {
            ErrorMessage = string.Empty;
            HasError = false;
        });

        private bool ValidateForm(string value) {
            ValidationTargetValue.Value = value;

            ValidationTargetValue.Validate();

            bool validationResult = ValidationTargetValue.IsValid;

            return validationResult;
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
            set { SetProperty(ref _hasError, value); }
        }


        DriveAutoSearch _resultSelected;
        public DriveAutoSearch ResultSelected {
            get { return _resultSelected; }
            set {
                SetProperty(ref _resultSelected, value);
                OnResulSelected(value);
            }
        }

        ObservableCollection<DriveAutoSearch> _foundResult;
        public ObservableCollection<DriveAutoSearch> FoundResult {
            get { return _foundResult; }
            set {
                SetProperty(ref _foundResult, value);
            }
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public SearchViewModel(IDriveAutoService driveAutoService) {
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
                    .Subscribe(foundResult => {
                        ApplySearchResults(foundResult);
                    });
            }
            catch (Exception ex) {
                Debug.WriteLine($"---ERROR: {ex.Message}");
            }

            SetupValidations();
        }

        public async Task<IEnumerable<DriveAutoSearch>> SearchInfoAsync(string value) {
            ResetCancellationTokenSource(ref _getCarsCancellationTokenSource);
            CancellationTokenSource cancellationTokenSource = _getCarsCancellationTokenSource;

            IEnumerable<DriveAutoSearch> carInfos = null;

            if (!string.IsNullOrEmpty(value)) {
                try {
                    carInfos = await _driveAutoService.GetCarNumbersAutocompleteAsync(value, cancellationTokenSource);
                }
                catch (Exception ex) {
                    Debug.WriteLine($"ERROR: {ex.Message}");
                    ErrorMessage = ex.Message;
                    HasError = string.IsNullOrEmpty(ErrorMessage);
                    Debugger.Break();
                }
            } else {
                carInfos = new DriveAutoSearch[] { };
            }

            return carInfos;
        }

        private async Task<IEnumerable<DriveAuto>> GetAllDriveAutoByNumber(string targetCarNumber) {
            ResetCancellationTokenSource(ref _getCarsCancellationTokenSource);
            CancellationTokenSource cancellationTokenSource = _getCarsCancellationTokenSource;

            IEnumerable<DriveAuto> result = null;

            Guid busyKey = Guid.NewGuid();
            UpdateBusyVisualState(busyKey, true);

            try {
                result = await _driveAutoService.GetAllDriveAutosAsync(targetCarNumber, cancellationTokenSource);
            }
            catch (Exception ex) {
                ErrorMessage = ex.Message;
                Debug.WriteLine($"ERROR: {ex.Message}");
                HasError = true;
            }

            UpdateBusyVisualState(busyKey, false);

            return result;
        }

        public override Task InitializeAsync(object navigationData) {

            ClearFoundedResult();

            return base.InitializeAsync(navigationData);
        }

        private void SetupValidations() {
            // ValidationTargetValue.Validations.Add(new LengthRule<string>() { ValidationMessage = "Мін. к-сть 4 символа" });
        }

        private void ApplySearchResults(IEnumerable<DriveAutoSearch> foundResult) {
            FoundResult = foundResult.ToObservableCollection();
        }

        private async void OnResulSelected(DriveAutoSearch value) {
            if (value != null) {
                await NavigationService.NavigateToAsync<FoundDriveAutoViewModel>(value.Number);
                ResultSelected = null;
            }
        }

        private void ClearFoundedResult() {
            FoundResult?.Clear();
            TargetValue = string.Empty;
        }
    }
}
