using Drive.Client.Extensions;
using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.Services.Announcement;
using Drive.Client.Services.OpenUrl;
using Drive.Client.ViewModels.Base;
using Stormlion.PhotoBrowser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels.Home.Post {
    public class PostBaseViewModel : NestedViewModel {

        private readonly IOpenUrlService _openUrlService;

        private readonly IAnnouncementService _announcementService;

        string _authorAvatarUrl;
        public string AuthorAvatarUrl {
            get => _authorAvatarUrl;
            private set => SetProperty(ref _authorAvatarUrl, value);
        }

        string _sourceUrl;
        public string SourceUrl {
            get { return _sourceUrl; }
            set { SetProperty(ref _sourceUrl, value); }
        }

        ObservableCollection<string> _loadedImages;
        public ObservableCollection<string> LoadedImages {
            get => _loadedImages;
            set => SetProperty(ref _loadedImages, value);
        }

        int _imagePosition;
        public int ImagePosition {
            get { return _imagePosition; }
            set { SetProperty(ref _imagePosition, value); }
        }

        string _authorName;
        public string AuthorName {
            get => _authorName;
            private set => SetProperty(ref _authorName, value);
        }

        DateTime _publishDate;
        public DateTime PublishDate {
            get => _publishDate;
            private set => SetProperty(ref _publishDate, value);
        }

        string _mainMessage;
        public string MainMessage {
            get => _mainMessage;
            private set => SetProperty(ref _mainMessage, value);
        }

        long _commentsCount;
        public long CommentsCount {
            get { return _commentsCount; }
            set { SetProperty(ref _commentsCount, value); }
        }

        long _likesCount;
        public long LikesCount {
            get { return _likesCount; }
            set { SetProperty(ref _likesCount, value); }
        }

        int? _imagesCount;
        public int? ImagesCount {
            get { return _imagesCount; }
            set { SetProperty(ref _imagesCount, value); }
        }

        bool _isRemovable;
        public bool IsRemovable {
            get { return _isRemovable; }
            set { SetProperty(ref _isRemovable, value); }
        }

        bool _isLiked;
        public bool IsLiked {
            get { return _isLiked; }
            set { SetProperty(ref _isLiked, value); }
        }

        bool _canShowImageInfo;
        public bool CanShowImageInfo {
            get { return _canShowImageInfo; }
            set { SetProperty(ref _canShowImageInfo, value); }
        }

        /// <summary>
        /// Post instance.
        /// </summary>
        Announce _post;
        public Announce Post {
            get => _post;
            set {
                SetProperty(ref _post, value);
                OnPost(value);
            }
        }

        public ICommand LikeCommand => new Command(() => OnLike());

        public ICommand ShowImageCommand => new Command(() => OnShowImage());

        /// <summary>
        ///     ctor().
        /// </summary>
        public PostBaseViewModel() {
            _openUrlService = DependencyLocator.Resolve<IOpenUrlService>();
            _announcementService = DependencyLocator.Resolve<IAnnouncementService>();
        }

        protected virtual void OnPost(Announce post) {
            if (post != null) {
                AuthorAvatarUrl = post.AnnounceBody.AvatarUrl;
                AuthorName = post.AnnounceBody.UserName.TrimStart();
                PublishDate = post.AnnounceBody.Created;
                MainMessage = post.AnnounceBody.Content;
                CommentsCount = post.AnnounceBody.CommentsCount;
                LikesCount = post.AnnounceBody.LikesCount;
                SourceUrl = post.ImageUrl?.FirstOrDefault();
                IsLiked = post.IsLikedByUser;
                ImagesCount = post.ImageUrl?.Length;
                LoadedImages = post.ImageUrl?.ToObservableCollection();
                CanShowImageInfo = post.AnnounceBody.Type == AnnounceType.Image;
            } else {
                AuthorAvatarUrl = null;
                AuthorName = null;
                PublishDate = default(DateTime);
                MainMessage = null;
                CommentsCount = default(long);
                LikesCount = default(long);
                IsLiked = default(bool);
                SourceUrl = string.Empty;
                ImagesCount = default(int);
                LoadedImages?.Clear();
                CanShowImageInfo = default(bool);
            }
        }

        public override Task InitializeAsync(object navigationData) {


            return base.InitializeAsync(navigationData);
        }

        private void OnShowImage() {
            try {
                if (LoadedImages != null && LoadedImages.Any()) {
                    var browser = new PhotoBrowser();
                    List<Photo> photos = new List<Photo>();

                    foreach (var imageUrl in LoadedImages) {
                        photos.Add(new Photo { URL = imageUrl });
                    }

                    browser.Photos = photos;
                    browser.ActionButtonPressed = (x) => {
                        PhotoBrowser.Close();
                    };
                    browser.Show();
                }
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR:{ex.Message}");
            }
        }


        private void OnLike() {
            _announcementService.SetLikeStatusAsync(Post.AnnounceBody.Id, new CancellationTokenSource());
        }
    }
}
