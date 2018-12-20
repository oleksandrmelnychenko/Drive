using Drive.Client.Factories.Announcements;
using Drive.Client.Helpers;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.Models.EntityModels.Announcement.Comments;
using Drive.Client.Models.Rest;
using Drive.Client.Services.Announcement;
using Drive.Client.Services.Signal.Announcement;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.BottomTabViewModels.Home.Post;
using Drive.Client.ViewModels.Posts;
using Drive.Client.Views.BottomTabViews.Home;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Drive.Client.ViewModels.BottomTabViewModels.Home {
    public sealed class HomeViewModel : TabbedViewModelBase, ISwitchTab {

        private readonly IAnnouncementService _announcementService;

        private readonly IAnnouncementsFactory _announcementsFactory;

        private readonly IAnnouncementSignalService _announcementSignalService;

        ObservableCollection<PostBaseViewModel> _posts;
        public ObservableCollection<PostBaseViewModel> Posts {
            get => _posts;
            private set => SetProperty(ref _posts, value);
        }

        PostBaseViewModel _selectedPostViewModel;
        public PostBaseViewModel SelectedPostViewModel {
            get { return _selectedPostViewModel; }
            set {
                if (SetProperty(ref _selectedPostViewModel, value)) {
                    if (value != null) {
                        NavigationService.NavigateToAsync<PostCommentsViewModel>(value);
                    }
                }
            }
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public HomeViewModel(IAnnouncementService announcementService,
                             IAnnouncementsFactory announcementsFactory,
                             IAnnouncementSignalService announcementSignalService) {
            _announcementService = announcementService;
            _announcementsFactory = announcementsFactory;
            _announcementSignalService = announcementSignalService;
        }

        protected override void TabbViewModelInit() {
            RelativeViewType = typeof(HomeView);
            TabIcon = IconPath.HOME;
            HasBackgroundItem = false;
        }

        protected override void OnSubscribeOnAppEvents() {
            base.OnSubscribeOnAppEvents();

            _announcementSignalService.GetAnnouncement += GetAnnouncements;
            _announcementSignalService.NewAnnounceReceived += NewAnnounceReceived;
            _announcementSignalService.PostCommentsCountReceived += PostCommentsCountReceived;
        }

        protected override void OnUnsubscribeFromAppEvents() {
            base.OnUnsubscribeFromAppEvents();

            _announcementSignalService.GetAnnouncement -= GetAnnouncements;
            _announcementSignalService.NewAnnounceReceived -= NewAnnounceReceived;
            _announcementSignalService.PostCommentsCountReceived -= PostCommentsCountReceived;
        }

        private void PostCommentsCountReceived(object sender, CommentCountBody e) {
            if (Posts == null) return;

            foreach (var post in Posts) {
                if (post.Post.AnnounceBody.Id == e.PostId) {
                    post.CommentsCount = e.CommentsCount;
                }
            }
        }

        private void NewAnnounceReceived(object sender, Announce e) {
            Posts?.Insert(0, _announcementsFactory.CreatePostViewModel(e));
        }

        private void GetAnnouncements(object sender, Announce[] e) {
            Posts = _announcementsFactory.BuildPostViewModels(e);
        }

        public override Task InitializeAsync(object navigationData) {
            if (navigationData is SelectedBottomBarTabArgs) {
                _announcementService.AskToGetAnnouncementAsync(new CancellationTokenSource());
            }

            return base.InitializeAsync(navigationData);
        }

        public override void Dispose() {
            base.Dispose();

            Posts?.Clear();
        }

        public void ClearAfterTabTap() {
        }

        public void TabClicked() {
        }
    }
}
