using Drive.Client.Models.EntityModels.Announcement.Comments;
using Drive.Client.ViewModels.Posts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Drive.Client.Factories.Comments {
    public class CommentsFactory : ICommentsFactory {
        public ObservableCollection<CommentViewModel> BuildCommentsViewModels(IEnumerable<Comment> comments) {
            ObservableCollection<CommentViewModel> commentViewModels = new ObservableCollection<CommentViewModel>();

            List<Comment> listedComments = comments.ToList();

            var sortedByDate = from a in listedComments
                               orderby a.Created descending
                               select a;

            foreach (var comment in sortedByDate) {
                CommentViewModel commentViewModel = new CommentViewModel {
                    AuthorAvatarUrl = comment.AvatarUrl,
                    Comment = comment.TextContent,
                    PublishDate = comment.Created,
                    AuthorName = comment.UserName
                };
                commentViewModels.Add(commentViewModel);
            }

            return commentViewModels;
        }

        public CommentViewModel CreateCommentViewModel(Comment e) {
            return new CommentViewModel {
                AuthorAvatarUrl = e.AvatarUrl,
                Comment = e.TextContent,
                PublishDate = e.Created,
                AuthorName = e.UserName
            };
        }
    }
}
