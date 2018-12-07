using Drive.Client.ViewModels.Base;
using System;

namespace Drive.Client.ViewModels.Posts {
    internal sealed class CommentViewModel : NestedViewModel {

        string _authorAvatarUrl;
        public string AuthorAvatarUrl {
            get => _authorAvatarUrl;
            set => SetProperty(ref _authorAvatarUrl, value);
        }

        string _authorName;
        public string AuthorName {
            get => _authorName;
            set => SetProperty(ref _authorName, value);
        }

        DateTime _publishDate;
        public DateTime PublishDate {
            get => _publishDate;
            set => SetProperty(ref _publishDate, value);
        }

        string _comment;
        public string Comment {
            get => _comment;
            set => SetProperty(ref _comment, value);
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public CommentViewModel() {

        }
    }
}
