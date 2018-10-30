using Drive.Client.Models.Identities.Posts;
using Drive.Client.Services.OpenUrl;
using Drive.Client.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.Post {
    public sealed class PostViewModel : NestedViewModel {

        private readonly IOpenUrlService _openUrlService;

        /// <summary>
        /// Post instance.
        /// </summary>
        PostBase _post;
        public PostBase Post {
            get => _post;
            set { SetProperty(ref _post, value); }
        }

   
        /// <summary>
        /// Open link post command.
        /// </summary>
        public ICommand OpenLinkPostCommand => new Command(async () => {
            try {
                if (Post.PostType == PostTypes.LinkPost) {
                    _openUrlService.OpenUrl("");
                }
            }
            catch (Exception) {
                await DialogService.ToastAsync("Sorry. Invalid link source");
            }
        });

        /// <summary>
        ///     ctor().
        /// </summary>
        public PostViewModel(IOpenUrlService openUrlService) {
            _openUrlService =openUrlService;
        }

        public override Task InitializeAsync(object navigationData) {


            return base.InitializeAsync(navigationData);
        }
    }
}
