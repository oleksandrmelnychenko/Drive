using Drive.Client.Extensions;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.Models.EntityModels.Cognitive;
using Drive.Client.Models.EntityModels.Identity;
using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Models.Medias;
using Drive.Client.Resources.Resx;
using Drive.Client.Services.Automobile;
using Drive.Client.Services.Media;
using Drive.Client.Services.Vision;
using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels {
    public sealed class DriveAutoDetailsViewModel : ContentPageBaseViewModel {

        private readonly IVisionService _visionService;

        private readonly IPickMediaService _pickMediaService;

        private readonly IDriveAutoService _driveAutoService;

        private CancellationTokenSource _analysePhotoCancellationTokenSource = new CancellationTokenSource();

        bool _hasResult;
        public bool HasResult {
            get { return _hasResult; }
            set { SetProperty(ref _hasResult, value); }
        }

        bool _isBackButtonAvailable;
        public bool IsBackButtonAvailable {
            get { return _isBackButtonAvailable; }
            set { SetProperty(ref _isBackButtonAvailable, value); }
        }

        ObservableCollection<DriveAuto> _driveAutoDetails = new ObservableCollection<DriveAuto>();
        public ObservableCollection<DriveAuto> DriveAutoDetails {
            get { return _driveAutoDetails; }
            set { SetProperty(ref _driveAutoDetails, value); }
        }

        string _errorMessage;
        public string ErrorMessage {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        /// <summary>
        ///     ctor();
        /// </summary>
        public DriveAutoDetailsViewModel(IPickMediaService pickMediaService,
                                         IVisionService visionService,
                                         IDriveAutoService driveAutoService) {
            _visionService = visionService;
            _pickMediaService = pickMediaService;
            _driveAutoService = driveAutoService;

            ActionBarViewModel = DependencyLocator.Resolve<CommonActionBarViewModel>();

            IsBackButtonAvailable = NavigationService.IsBackButtonAvailable;
        }

        public override void Dispose() {
            base.Dispose();

            _driveAutoDetails?.Clear();
            ErrorMessage = string.Empty;
            ResetCancellationTokenSource(ref _analysePhotoCancellationTokenSource);

            ActionBarViewModel?.Dispose();
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is SearchByImageArgs) {
                AnalysePhotoAsync();
            }

            if (navigationData is DriveAuto driveAuto) {
                DriveAutoDetails?.Add(driveAuto);
                HasResult = true;
            }

            ActionBarViewModel?.InitializeAsync(navigationData);

            return base.InitializeAsync(navigationData);
        }

        private async void AnalysePhotoAsync() {
            Guid busyKey = Guid.NewGuid();
            SetBusy(busyKey, true);
            PickedImage targetImage = null;

            ResetCancellationTokenSource(ref _analysePhotoCancellationTokenSource);
            CancellationTokenSource cancellationTokenSource = _analysePhotoCancellationTokenSource;

            try {
                using (var file = await _pickMediaService.TakePhotoAsync()) {
                    if (file != null) {
                        List<string> results = await _visionService.AnalyzeImageForText(file);

                        if (results != null && results.Any()) {
                            targetImage = await _pickMediaService.BuildPickedImageAsync(file);

                            if (targetImage != null) {
                                FormDataContent formDataContent = new FormDataContent {
                                    Content = results,
                                    MediaContent = targetImage
                                };

                                List<DriveAuto> driveAutoDetails =
                                    await _driveAutoService.SearchDriveAutoByCognitiveAsync(formDataContent, cancellationTokenSource.Token);

                                if (driveAutoDetails != null) {
                                    DriveAutoDetails = driveAutoDetails.ToObservableCollection();
                                    HasResult = true;
                                } else {
                                    HasResult = false;
                                    ErrorMessage = string.Empty;
                                }
                            }
                        } else {
                            HasResult = false;
                            ErrorMessage = ResourceLoader.GetString(nameof(AppStrings.TryMore)).Value;
                        }
                        file.Dispose();
                    }
                }
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR: -{ex.Message}");
                Debugger.Break();
                try {
                    HttpRequestExceptionResult httpRequestExceptionResult = JsonConvert.DeserializeObject<HttpRequestExceptionResult>(ex.Message);
                    HasResult = false;
                    ErrorMessage = httpRequestExceptionResult.Message;
                }
                catch (Exception exc) {
                    Debug.WriteLine($"ERROR: -{exc.Message}");
                    Debugger.Break();
                    BackCommand.Execute(null);
                }
            }
            SetBusy(busyKey, false);

            if (targetImage == null) {
                BackCommand.Execute(null);
            }
        }
    }
}
