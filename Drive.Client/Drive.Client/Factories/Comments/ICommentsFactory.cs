using Drive.Client.Models.EntityModels.Announcement.Comments;
using Drive.Client.ViewModels.Posts;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Drive.Client.Factories.Comments {
    public interface ICommentsFactory {
        ObservableCollection<CommentViewModel> BuildCommentsViewModels(IEnumerable<Comment> comments);

        CommentViewModel CreateCommentViewModel(Comment e);
    }
}
