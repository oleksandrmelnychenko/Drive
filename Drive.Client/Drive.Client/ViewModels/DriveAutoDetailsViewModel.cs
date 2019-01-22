using Drive.Client.Models.EntityModels.Cognitive;
using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Models.Medias;
using Drive.Client.Services.Automobile;
using Drive.Client.Services.Media;
using Drive.Client.Services.Vision;
using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels {
    public sealed class DriveAutoDetailsViewModel : ContentPageBaseViewModel {

        private readonly IVisionService _visionService;

        private readonly IPickMediaService _pickMediaService;

        private readonly IDriveAutoService _driveAutoService;

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

            ActionBarViewModel?.Dispose();
        }

        public override Task InitializeAsync(object navigationData) {

            // temp variable
            if (navigationData is bool canTakePhoto) {
                if (canTakePhoto) {
                    AnalysePhotoAsync();
                }
            }

            if (navigationData is DriveAuto driveAuto) {
                DriveAutoDetails?.Add(driveAuto);
            }

            ActionBarViewModel?.InitializeAsync(navigationData);

            return base.InitializeAsync(navigationData);
        }

        private async void AnalysePhotoAsync() {
            try {
                Guid busyKey = Guid.NewGuid();
                SetBusy(busyKey, true);

                using (var file = await _pickMediaService.TakePhotoAsync()) {
                    if (file != null) {
                        List<string> results = await _visionService.AnalyzeImageForText(file);

                        if (results != null) {
                            PickedImage targetImage = await _pickMediaService.BuildPickedImageAsync(file);

                            if (targetImage != null) {
                                FormDataContent formDataContent = new FormDataContent {
                                    Content = results,
                                    MediaContent = targetImage
                                };

                                var tt = await _driveAutoService.SearchDriveAutoByCognitiveAsync(formDataContent);

                                if (tt != null) {

                                }
                            }
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

        private void GetResult(List<string> results, PickedImage targetImage) {

        }
    }
}
