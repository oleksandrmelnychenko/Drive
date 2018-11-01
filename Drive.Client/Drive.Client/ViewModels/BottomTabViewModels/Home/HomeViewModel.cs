using Drive.Client.Helpers;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.Models.Identities.Posts;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.BottomTabViewModels.Home.Post;
using Drive.Client.Views.BottomTabViews.Home;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.BottomTabViewModels.Home {
    public sealed class HomeViewModel : TabbedViewModelBase {

        SinglePostBaseViewModel[] _posts = new SinglePostBaseViewModel[] { };
        public SinglePostBaseViewModel[] Posts {
            get => _posts;
            private set => SetProperty(ref _posts, value);
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public HomeViewModel() {

        }

        protected override void TabbViewModelInit() {
            RelativeViewType = typeof(HomeView);
            TabIcon = IconPath.HOME;
            HasBackgroundItem = false;
        }


        protected override void OnSubscribeOnAppEvents() {
            base.OnSubscribeOnAppEvents();


        }

        protected override void OnUnsubscribeFromAppEvents() {
            base.OnUnsubscribeFromAppEvents();


        }

        public override Task InitializeAsync(object navigationData) {
            if (navigationData is SelectedBottomBarTabArgs) {
                List<SinglePostBaseViewModel> foundPosts = new List<SinglePostBaseViewModel>();

                for (int i = 0; i < 49; i++) {
                    PostBase postBase = null;
                    SinglePostBaseViewModel postViewModel = null;

                    if (i % 2 == 0) {
                        postBase = new TextPost();
                        postViewModel = DependencyLocator.Resolve<TextPostViewModel>();
                    }
                    else {
                        postBase = new MediaPost();
                        postViewModel = DependencyLocator.Resolve<MediaPostViewModel>();

                        if (i % 3 == 0) {
                            ((MediaPost)postBase).MediaUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRoQ_1qSVR7vwVmYg_WLDZJVnyDc_-Qg8yC1neV90WEFLon3Zz_xw";
                        }
                    }

                    postBase.AuthorName = string.Format("{0} {1}", postBase.AuthorName, i);
                    postBase.CommentsCount = i;
                    for (int m = 0; m < i; m++) {
                        postBase.PostMessage = string.Format("{0}. {1}", postBase.AuthorName, postBase.AuthorName);
                    }
                    postBase.PublishDate = postBase.PublishDate - TimeSpan.FromHours(i);

                    postViewModel.Post = postBase;
                    foundPosts.Add(postViewModel);
                }

                Posts = foundPosts.ToArray();
            }

            return base.InitializeAsync(navigationData);
        }
    }
}
