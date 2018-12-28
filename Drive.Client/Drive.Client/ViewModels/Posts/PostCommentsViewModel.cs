using Drive.Client.Factories.Comments;
using Drive.Client.Factories.Validation;
using Drive.Client.Models.EntityModels.Announcement.Comments;
using Drive.Client.Resources.Resx;
using Drive.Client.Services.Comments;
using Drive.Client.Services.Signal.Announcement;
using Drive.Client.Validations;
using Drive.Client.Validations.ValidationRules;
using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.BottomTabViewModels.Home.Post;
using Drive.Client.ViewModels.Popups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.Posts {
    internal sealed class PostCommentsViewModel : ContentPageBaseViewModel {

        private readonly ICommentService _commentService;

        private readonly ICommentsFactory _commentsFactory;

        private readonly IValidationObjectFactory _validationObjectFactory;

        private readonly IAnnouncementSignalService _announcementSignalService;

        PostBaseViewModel _currentPost;
        public PostBaseViewModel CurrentPost {
            get { return _currentPost; }
            set { SetProperty(ref _currentPost, value); }
        }

        ObservableCollection<CommentViewModel> _comments;
        public ObservableCollection<CommentViewModel> Comments {
            get { return _comments; }
            set { SetProperty(ref _comments, value); }
        }

        ValidatableObject<string> _commentText;
        public ValidatableObject<string> CommentText {
            get { return _commentText; }
            set { SetProperty(ref _commentText, value); }
        }

        PostFunctionPopupViewModel _postFunctionPopupViewModel;
        public PostFunctionPopupViewModel PostFunctionPopupViewModel {
            get => _postFunctionPopupViewModel;
            private set {
                _postFunctionPopupViewModel?.Dispose();
                SetProperty(ref _postFunctionPopupViewModel, value);
            }
        }

        public ICommand SendCommand => new Command(() => OnSend());

        public ICommand OpenFunctionCommand => new Command(() => {
            PostFunctionPopupViewModel.ShowPopupCommand.Execute(null);
            PostFunctionPopupViewModel.InitializeAsync(CurrentPost);
        });

        /// <summary>
        ///     ctor().
        /// </summary>
        public PostCommentsViewModel(ICommentService commentService,
                                     ICommentsFactory commentsFactory,
                                     IValidationObjectFactory validationObjectFactory,
                                     IAnnouncementSignalService announcementSignalService) {
            _commentService = commentService;
            _commentsFactory = commentsFactory;
            _validationObjectFactory = validationObjectFactory;
            _announcementSignalService = announcementSignalService;

            _commentText = validationObjectFactory.GetValidatableObject<string>();

            AddValidationRules();

            PostFunctionPopupViewModel = DependencyLocator.Resolve<PostFunctionPopupViewModel>();
            PostFunctionPopupViewModel.InitializeAsync(this);

            ActionBarViewModel = DependencyLocator.Resolve<CommonActionBarViewModel>();
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is PostBaseViewModel postBaseViewModel) {
                CurrentPost = postBaseViewModel;
                GetPostComments(postBaseViewModel);
            }

            PostFunctionPopupViewModel?.InitializeAsync(navigationData);

            return base.InitializeAsync(navigationData);
        }

        public override void Dispose() {
            base.Dispose();

            ResetCancellationTokenSource(ref _getPostCommentsCancellationTokenSource);
            Comments?.Clear();
        }

        private CancellationTokenSource _getPostCommentsCancellationTokenSource = new CancellationTokenSource();

        private async void GetPostComments(PostBaseViewModel postBaseViewModel) {
            try {
                ResetCancellationTokenSource(ref _getPostCommentsCancellationTokenSource);
                CancellationTokenSource cancellationTokenSource = _getPostCommentsCancellationTokenSource;

                var comments = await _commentService.GetPostCommentsAsync(postBaseViewModel.Post.AnnounceBody.Id, cancellationTokenSource);

                if (comments != null) {
                    Comments = _commentsFactory.BuildCommentsViewModels(comments);
                }
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR:{ex.Message}");
            }
        }

        protected override void OnSubscribeOnAppEvents() {
            base.OnSubscribeOnAppEvents();

            _announcementSignalService.NewPostCommentReceived += NewPostCommentReceived;
            _announcementSignalService.PostCommentsCountReceived += PostCommentsCountReceived;
        }

        protected override void OnUnsubscribeFromAppEvents() {
            base.OnUnsubscribeFromAppEvents();

            _announcementSignalService.NewPostCommentReceived -= NewPostCommentReceived;
            _announcementSignalService.PostCommentsCountReceived -= PostCommentsCountReceived;
        }

        private void PostCommentsCountReceived(object sender, CommentCountBody e) {
            if (CurrentPost.Post.AnnounceBody.Id.Equals(e.PostId)) {
                CurrentPost.CommentsCount = e.CommentsCount;
            }
        }

        private void NewPostCommentReceived(object sender, Comment e) {
            Comments?.Add(_commentsFactory.CreateCommentViewModel(e));
        }
     
        private void OnSend() {
            if (Validate()) {
                CommentBody commentBody = new CommentBody {
                    PostId = CurrentPost.Post.AnnounceBody.Id,                   
                    TextContent = CommentText.Value
                };

                _commentService.SendCommentAsync(commentBody, new CancellationTokenSource());

                CommentText.Value = string.Empty;
            }
        }

        private bool Validate() {
            bool isValid = default(bool);

            CommentText.Validate();

            isValid = CommentText.IsValid;

            return isValid;
        }

        private void AddValidationRules() {
            _commentText.Validations.Add(new IsNotNullOrEmptyRule<string>());
        }
    }
}
