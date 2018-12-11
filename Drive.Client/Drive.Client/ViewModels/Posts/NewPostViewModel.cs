using Drive.Client.Factories.Validation;
using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.Services.Media;
using Drive.Client.Validations;
using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.Base;
using Microsoft.AppCenter.Crashes;
using Plugin.Media.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.Posts {
    internal sealed class NewPostViewModel : ContentPageBaseViewModel, IInputForm, IBuildFormModel {

        private readonly IPickMediaService _pickMediaService;
        private readonly IValidationObjectFactory _validationObjectFactory;

        public NewPostViewModel(
            IPickMediaService pickMediaService,
            IValidationObjectFactory validationObjectFactory) {

            _pickMediaService = pickMediaService;
            _validationObjectFactory = validationObjectFactory;

            ResetValidationObjects();
            AttachedPostMedias = new ObservableCollection<AttachedAnnounceMediaBase>();

            ActionBarViewModel = DependencyLocator.Resolve<NewPostActionBarViewModel>();
        }

        public ICommand AnnounceTextChangedCommand => new Command(() => {
            try {
                if (!string.IsNullOrEmpty(AnnounceText.Value) && !string.IsNullOrWhiteSpace(AnnounceText.Value)) {
                    if (TargetAnnounceType == AnnounceType.Video || TargetAnnounceType == AnnounceType.Image) {
                        ((NewPostActionBarViewModel)ActionBarViewModel).ResolveExecutionAvailability(AttachedPostMedias?.Any());
                    }
                    else {
                        ((NewPostActionBarViewModel)ActionBarViewModel).ResolveExecutionAvailability(true);
                    }
                }
                else {
                    ((NewPostActionBarViewModel)ActionBarViewModel).ResolveExecutionAvailability(false);
                }
            }
            catch (Exception exc) {
                Crashes.TrackError(exc);
            }
        });

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

        private AnnounceType _targetAnnounceType;
        public AnnounceType TargetAnnounceType {
            get => _targetAnnounceType;
            private set => SetProperty<AnnounceType>(ref _targetAnnounceType, value);
        }

        private ValidatableObject<string> _announceText;
        public ValidatableObject<string> AnnounceText {
            get { return _announceText; }
            set { SetProperty(ref _announceText, value); }
        }

        private ObservableCollection<AttachedAnnounceMediaBase> _attachedPostMedias;
        public ObservableCollection<AttachedAnnounceMediaBase> AttachedPostMedias {
            get => _attachedPostMedias;
            private set {
                if (_attachedPostMedias != null) {
                    _attachedPostMedias.CollectionChanged -= OnAttachedPostMediasCollectionChanged;
                }

                SetProperty<ObservableCollection<AttachedAnnounceMediaBase>>(ref _attachedPostMedias, value);

                if (_attachedPostMedias != null) {
                    _attachedPostMedias.CollectionChanged += OnAttachedPostMediasCollectionChanged;
                }

                AnnounceTextChangedCommand.Execute(null);
            }
        }

        private AttachedAnnounceMediaBase _selectedAttachedPostMedia;
        public AttachedAnnounceMediaBase SelectedAttachedPostMedia {
            get => _selectedAttachedPostMedia;
            set => SetProperty<AttachedAnnounceMediaBase>(ref _selectedAttachedPostMedia, value);
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is AnnounceType postTypeNavigationData) {
                TargetAnnounceType = postTypeNavigationData;
            }

            return base.InitializeAsync(navigationData);
        }

        public object BuildFormModel() {
            AnnounceBody announce = new AnnounceBody() {
                Content = AnnounceText.Value,
                Type = TargetAnnounceType
            };

            return announce;
        }

        public bool ValidateForm() {
            bool isValid = false;

            isValid = AnnounceText.Validate();

            if (TargetAnnounceType == AnnounceType.Video || TargetAnnounceType == AnnounceType.Image) {
                isValid = isValid && AttachedPostMedias.Any();
            }

            return isValid;
        }

        public void ResetInputForm() {
            ResetValidationObjects();
        }

        private void ResetValidationObjects() {
            AnnounceText = _validationObjectFactory.GetValidatableObject<string>();
        }

        private void OnAttachedPostMediasCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => AnnounceTextChangedCommand.Execute(null);
    }
}
