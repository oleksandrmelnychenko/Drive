using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.Services.Media;
using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.Base;
using Microsoft.AppCenter.Crashes;
using Plugin.Media.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.Posts {
    internal sealed class NewPostViewModel : ContentPageBaseViewModel {

        private readonly IPickMediaService _pickMediaService;

        public NewPostViewModel(
            IPickMediaService pickMediaService) {

            _pickMediaService = pickMediaService;

            ActionBarViewModel = DependencyLocator.Resolve<NewPostActionBarViewModel>();
        }

        public ICommand DeleteAttachedMediaCommand => new Command((object parameter) => {
            if (parameter is AttachedAnnounceMediaBase attachedMedia) {
                try {
                    AttachedPostMedias.Remove(attachedMedia);
                }
                catch (Exception exc) {
                    Debugger.Break();
                    Crashes.TrackError(exc);
                }
            }
        });

        public ICommand AttachNewMediaCommand => new Command(async () => {
            Guid busyKey = Guid.NewGuid();
            SetBusy(busyKey, true);

            try {
                MediaFile mediaFile = await _pickMediaService.PickPhotoAsync();

                if (mediaFile != null) {
                    Stream mediaStream = mediaFile.GetStream();

                    AttachedPostMedias.Add(new AttachedImage() { MediaPresentation = await _pickMediaService.BuildImageSourceAsync(mediaStream), DataBase64 = await _pickMediaService.ParseStreamToBase64(mediaStream) });
                    mediaStream.Close();
                    mediaStream.Dispose();
                    mediaFile.Dispose();
                }
            }
            catch (Exception exc) {
                Debugger.Break();
                Crashes.TrackError(exc);
            }

            SetBusy(busyKey, false);
        });

        private AnnounceType _targetPostType;
        public AnnounceType TargetPostType {
            get => _targetPostType;
            private set => SetProperty<AnnounceType>(ref _targetPostType, value);
        }

        private ObservableCollection<AttachedAnnounceMediaBase> _attachedPostMedias = new ObservableCollection<AttachedAnnounceMediaBase>();
        public ObservableCollection<AttachedAnnounceMediaBase> AttachedPostMedias {
            get => _attachedPostMedias;
            private set => SetProperty<ObservableCollection<AttachedAnnounceMediaBase>>(ref _attachedPostMedias, value);
        }

        private AttachedAnnounceMediaBase _selectedAttachedPostMedia;
        public AttachedAnnounceMediaBase SelectedAttachedPostMedia {
            get => _selectedAttachedPostMedia;
            set => SetProperty<AttachedAnnounceMediaBase>(ref _selectedAttachedPostMedia, value);
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is AnnounceType postTypeNavigationData) {
                TargetPostType = postTypeNavigationData;
            }

            return base.InitializeAsync(navigationData);
        }
    }
}
