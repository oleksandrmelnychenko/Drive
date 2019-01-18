using Drive.Client.Extensions;
using Drive.Client.Helpers;
using Drive.Client.Helpers.Localize;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.Resources.Resx;
using Drive.Client.Services.Media;
using Drive.Client.Services.Vision;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.IdentityAccounting.Registration;
using Drive.Client.Views.BottomTabViews.Bookmark;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels.Bookmark
{
    public sealed class SavedViewModel : NestedViewModel, IVisualFiguring, ISwitchTab {

        private readonly IVisionService _visionService;

        private readonly IPickMediaService _pickMediaService;

        public Type RelativeViewType => typeof(SavedView);

        bool _visibilityClosedView;
        public bool VisibilityClosedView {
            get { return _visibilityClosedView; }
            set { SetProperty(ref _visibilityClosedView, value); }
        }

        StringResource _tabHeader;
        public StringResource TabHeader {
            get => _tabHeader;
            private set => SetProperty(ref _tabHeader, value);
        }

        ObservableCollection<string> _results;
        public ObservableCollection<string> Results {
            get { return _results; }
            set { SetProperty(ref _results, value); }
        }

        public ICommand SignInCommand => new Command(async () => await NavigationService.NavigateToAsync<SignInPhoneNumberStepViewModel>());

        public ICommand SignUpCommand => new Command(async () => await NavigationService.NavigateToAsync<PhoneNumberRegisterStepViewModel>());

        public ICommand TestCommand => new Command(async () => await OnTestAsync());

        /// <summary>
        ///     ctor().
        /// </summary>
        public SavedViewModel(IPickMediaService pickMediaService, IVisionService visionService) {
            _visionService = visionService;
            _pickMediaService = pickMediaService;
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is SelectedBottomBarTabArgs) {
            }

            UpdateView();

            return base.InitializeAsync(navigationData);
        }

        private async Task OnTestAsync() {
            try {
                //await NavigationService.NavigateToAsync<TestViewModel>();

                using (var file = await _pickMediaService.TakePhotoAsync()) {
                    if (file != null) {
                        var result = await _visionService.AnalyzeImageForText(file);

                        if (result != null) {
                            Results = result.ToObservableCollection();
                        }
                    }
                }

                //using (var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Medium, CompressionQuality = 90 })) {

                //    if (file != null) {
                //        var result = await _visionService.AnalyzeImageForText(file);

                //        if (result != null) {

                //        }
                //    }
                //}
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR: -{ex.Message}");
                Debugger.Break();
            }
        }

        protected override void ResolveStringResources() {
            base.ResolveStringResources();

            TabHeader = ResourceLoader.GetString(nameof(AppStrings.SavedUpperCase));
        }

        private void UpdateView() {
            VisibilityClosedView = !BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth;
        }

        public void ClearAfterTabTap() {
        }

        public void TabClicked() {
        }
    }
}
