﻿using Drive.Client.Factories.Announcements;
using Drive.Client.Helpers;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.Models.EntityModels.Announcement.Comments;
using Drive.Client.Models.Rest;
using Drive.Client.Services.Announcement;
using Drive.Client.Services.Signal.Announcement;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.BottomTabViewModels.Home.Post;
using Drive.Client.ViewModels.IdentityAccounting.Registration;
using Drive.Client.ViewModels.Popups;
using Drive.Client.ViewModels.Posts;
using Drive.Client.Views.BottomTabViews.Home;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Drive.Client.ViewModels.BottomTabViewModels.Home {
    public sealed class HomeViewModel : TabbedViewModelBase {

        //private readonly IAnnouncementService _announcementService;

        //private readonly IAnnouncementsFactory _announcementsFactory;

        //private readonly IAnnouncementSignalService _announcementSignalService;

        //private CancellationTokenSource _getPostsCancellationTokenSource = new CancellationTokenSource();

        //ObservableCollection<PostBaseViewModel> _posts;
        //public ObservableCollection<PostBaseViewModel> Posts {
        //    get => _posts;
        //    private set => SetProperty(ref _posts, value);
        //}

        //PostBaseViewModel _selectedPostViewModel;
        //public PostBaseViewModel SelectedPostViewModel {
        //    get { return _selectedPostViewModel; }
        //    set {
        //        if (SetProperty(ref _selectedPostViewModel, value)) {
        //            if (value != null) {
        //                NavigationService.NavigateToAsync<PostCommentsViewModel>(value);
        //            }
        //        }
        //    }
        //}

        //bool _visibilityClosedView;
        //public bool VisibilityClosedView {
        //    get { return _visibilityClosedView; }
        //    set { SetProperty(ref _visibilityClosedView, value); }
        //}

        //PostFunctionPopupViewModel _postFunctionPopupViewModel;
        //public PostFunctionPopupViewModel PostFunctionPopupViewModel {
        //    get => _postFunctionPopupViewModel;
        //    private set {
        //        _postFunctionPopupViewModel?.Dispose();
        //        SetProperty(ref _postFunctionPopupViewModel, value);
        //    }
        //}

        //public ICommand SignInCommand => new Command(async () => await NavigationService.NavigateToAsync<SignInPhoneNumberStepViewModel>());

        //public ICommand SignUpCommand => new Command(async () => await NavigationService.NavigateToAsync<PhoneNumberRegisterStepViewModel>());

        //public ICommand RefreshPostsCommand => new Command(() => OnRefreshPostsCommand());

        //public ICommand OpenFunctionCommand => new Command((param) => OpenFunction(param));

        List<IVisualFiguring> _postTabs;
        public List<IVisualFiguring> PostTabs {
            get => _postTabs;
            private set {
                _postTabs?.ForEach(searchTab => searchTab.Dispose());
                SetProperty(ref _postTabs, value);
            }
        }

        int _selectedTabIndex;
        public int SelectedTabIndex {
            get => _selectedTabIndex;
            set => SetProperty(ref _selectedTabIndex, value);
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public HomeViewModel() {
            PostTabs = new List<IVisualFiguring>() {
                DependencyLocator.Resolve<BandViewModel>(),
                DependencyLocator.Resolve<SavedViewModel>()
            };
            PostTabs.ForEach(searchTab => searchTab.InitializeAsync(this));
        }

        protected override void TabbViewModelInit() {
            RelativeViewType = typeof(HomeView);
            TabIcon = IconPath.HOME;
            HasBackgroundItem = false;
        }

        //protected override void OnSubscribeOnAppEvents() {
        //    base.OnSubscribeOnAppEvents();

        //    _announcementSignalService.PostLikesCountReceived += PostLikesCountReceived;
        //    _announcementSignalService.NewAnnounceReceived += NewAnnounceReceived;
        //    _announcementSignalService.DeletedPostReceived += DeletedPostReceived;
        //    _announcementSignalService.PostCommentsCountReceived += PostCommentsCountReceived;
        //}

        //protected override void OnUnsubscribeFromAppEvents() {
        //    base.OnUnsubscribeFromAppEvents();

        //    _announcementSignalService.NewAnnounceReceived -= NewAnnounceReceived;
        //    _announcementSignalService.DeletedPostReceived -= DeletedPostReceived;
        //    _announcementSignalService.PostCommentsCountReceived -= PostCommentsCountReceived;
        //    _announcementSignalService.PostLikesCountReceived -= PostLikesCountReceived;
        //}

        public override Task InitializeAsync(object navigationData) {
            if (navigationData is SelectedBottomBarTabArgs) {

            } 

            PostTabs?.ForEach(searchTab => searchTab.InitializeAsync(navigationData));

            return base.InitializeAsync(navigationData);
        }

        public override void Dispose() {
            base.Dispose();

            PostTabs?.ForEach(searchTab => searchTab.Dispose());
        }

        //private async void OnRefreshPostsCommand() {
        //    IsBusy = true;

        //    ResetCancellationTokenSource(ref _getPostsCancellationTokenSource);
        //    CancellationTokenSource cancellationTokenSource = _getPostsCancellationTokenSource;

        //    await GetDataAsync(cancellationTokenSource);

        //    IsBusy = false;
        //}

        //private void PostLikesCountReceived(object sender, PostLikedBody e) {
        //    try {
        //        if (Posts == null) return;

        //        foreach (var post in Posts) {
        //            if (post.Post.AnnounceBody.Id == e.PostId) {
        //                post.LikesCount = e.LikesCount;
        //                if (e.PostLikedByUser.UserNetId == BaseSingleton<GlobalSetting>.Instance.UserProfile.NetId) {
        //                    post.IsLiked = e.PostLikedByUser.IsLikedByUser;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex) {
        //        Debug.WriteLine($"ERROR:{ex.Message}");
        //        Debugger.Break();
        //    }
        //}

        //private async void GetPosts() {
        //    try {
        //        ResetCancellationTokenSource(ref _getPostsCancellationTokenSource);
        //        CancellationTokenSource cancellationTokenSource = _getPostsCancellationTokenSource;

        //        Guid busyKey = Guid.NewGuid();
        //        UpdateBusyVisualState(busyKey, true);

        //        await GetDataAsync(cancellationTokenSource);

        //        UpdateBusyVisualState(busyKey, false);
        //    }
        //    catch (Exception ex) {
        //        Debug.WriteLine($"ERROR:{ex.Message}");
        //    }
        //}

        //private async Task GetDataAsync(CancellationTokenSource cancellationTokenSource) {
        //    var announces = await _announcementService.GetAnnouncesAsync(cancellationTokenSource);

        //    if (announces != null) {
        //        Posts = _announcementsFactory.BuildPostViewModels(announces);
        //    }
        //}

        //private void OpenFunction(object param) {
        //    if (param is PostBaseViewModel postBaseViewModelParam) {
        //        PostFunctionPopupViewModel.ShowPopupCommand.Execute(null);
        //        PostFunctionPopupViewModel.InitializeAsync(param);
        //    }
        //}

        //private void PostCommentsCountReceived(object sender, CommentCountBody e) {
        //    try {
        //        if (Posts == null) return;

        //        foreach (var post in Posts) {
        //            if (post.Post.AnnounceBody.Id == e.PostId) {
        //                post.CommentsCount = e.CommentsCount;
        //            }
        //        }
        //    }
        //    catch (Exception ex) {
        //        Debug.WriteLine($"ERROR:{ex.Message}");
        //        Debugger.Break();
        //    }
        //}

        //private void DeletedPostReceived(object sender, string e) {
        //    try {
        //        if (Posts == null) return;

        //        foreach (PostBaseViewModel post in Posts.ToList()) {
        //            if (post.Post.AnnounceBody.Id == e) {
        //                Posts.Remove(post);
        //            }
        //        }
        //    }
        //    catch (Exception ex) {
        //        Debug.WriteLine($"ERROR:{ex.Message}");
        //        Debugger.Break();
        //    }
        //}

        //private void NewAnnounceReceived(object sender, Announce e) {
        //    try {
        //        Posts?.Insert(0, _announcementsFactory.CreatePostViewModel(e));
        //    }
        //    catch (Exception ex) {
        //        Debug.WriteLine($"ERROR:{ex.Message}");
        //        Debugger.Break();
        //    }
        //}

        //private void UpdateView() {
        //    VisibilityClosedView = !BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth;
        //}
    }
}
