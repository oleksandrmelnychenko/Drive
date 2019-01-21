using Drive.Client.Extensions;
using Drive.Client.Helpers.Localize;
using Drive.Client.Models.EntityModels;
using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.Resources.Resx;
using Drive.Client.Services.Automobile;
using Drive.Client.Services.Media;
using Drive.Client.Services.Vision;
using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels {
    public sealed class FoundDriveAutoViewModel : ContentPageBaseViewModel {

        private CancellationTokenSource _getCarsCancellationTokenSource = new CancellationTokenSource();

        private readonly IVisionService _visionService;

        private readonly IPickMediaService _pickMediaService;

        private readonly IDriveAutoService _driveAutoService;

        bool _isBackButtonAvailable;
        public bool IsBackButtonAvailable {
            get { return _isBackButtonAvailable; }
            set { SetProperty(ref _isBackButtonAvailable, value); }
        }

        string _targetCarNumber;
        public string TargetCarNumber {
            get => _targetCarNumber;
            private set => SetProperty<string>(ref _targetCarNumber, value);
        }

        string _resultInfo;
        public string ResultInfo {
            get { return _resultInfo; }
            set { SetProperty(ref _resultInfo, value); }
        }

        bool _visibilityResultInfo;
        public bool VisibilityResultInfo {
            get { return _visibilityResultInfo; }
            set { SetProperty(ref _visibilityResultInfo, value); }
        }

        ObservableCollection<DriveAuto> _foundCars;
        public ObservableCollection<DriveAuto> FoundCars {
            get => _foundCars;
            private set => SetProperty(ref _foundCars, value);
        }

        DriveAuto _selectedCar;
        public DriveAuto SelectedCar {
            get { return _selectedCar; }
            set {
                if (SetProperty(ref _selectedCar, value) && value != null) {
                    NavigationService.NavigateToAsync<DriveAutoDetailsViewModel>(value);
                }
            }
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public FoundDriveAutoViewModel(IDriveAutoService driveAutoService,
                                       IPickMediaService pickMediaService,
                                       IVisionService visionService) {
            _driveAutoService = driveAutoService;
            _visionService = visionService;
            _pickMediaService = pickMediaService;

            ActionBarViewModel = DependencyLocator.Resolve<CommonActionBarViewModel>();

            IsBackButtonAvailable = NavigationService.IsBackButtonAvailable;
        }

        public override void Dispose() {
            base.Dispose();

            ResetCancellationTokenSource(ref _getCarsCancellationTokenSource);
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is bool canTakePhoto) {
                if (canTakePhoto) {
                    AnalysePhotoAsync();
                }
            }

            if (navigationData is string) {
                TargetCarNumber = navigationData.ToString();

                GetDriveAutoDetail(TargetCarNumber);
            }

            if (navigationData is GetAllArg getAllArg) {
                FoundCars = getAllArg.FoundCars;
                VisibilityResultInfo = !FoundCars.Any();
            }
            return base.InitializeAsync(navigationData);
        }

        protected override void ResolveStringResources() {
            base.ResolveStringResources();

            _resultInfo = (ResourceLoader.GetString(nameof(AppStrings.SearchResult)).Value);
        }

        private async void AnalysePhotoAsync() {
            try {
                Guid busyKey = Guid.NewGuid();
                SetBusy(busyKey, true);

                using (var file = await _pickMediaService.TakePhotoAsync()) {
                    if (file != null) {
                        var result = await _visionService.AnalyzeImageForText(file);

                        if (result != null) {
                            List<string> results = result;
                        }
                    }
                }
                SetBusy(busyKey, false);
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR: -{ex.Message}");
                Debugger.Break();
            }
        }

        private async void GetDriveAutoDetail(string targetCarNumber) {
            ResetCancellationTokenSource(ref _getCarsCancellationTokenSource);
            CancellationTokenSource cancellationTokenSource = _getCarsCancellationTokenSource;

            Guid busyKey = Guid.NewGuid();
            SetBusy(busyKey, true);

            try {
                IEnumerable<DriveAuto> result = await _driveAutoService.GetDriveAutoByNumberAsync(targetCarNumber, cancellationTokenSource.Token);

                if (result != null) {
                    FoundCars = result.ToObservableCollection();
                    VisibilityResultInfo = !FoundCars.Any();
                }
            }
            catch (OperationCanceledException ex) { Debug.WriteLine($"ERROR: {ex.Message}"); }
            catch (ObjectDisposedException ex) { Debug.WriteLine($"ERROR: {ex.Message}"); }

            catch (Exception ex) {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }
            SetBusy(busyKey, false);
        }
    }
}
