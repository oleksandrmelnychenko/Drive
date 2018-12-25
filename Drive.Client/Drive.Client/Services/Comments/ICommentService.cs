using Drive.Client.Models.EntityModels.Announcement.Comments;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.Comments {
    public interface ICommentService  {
        Task GetPostCommentsById(string postId, CancellationTokenSource cancellationTokenSource);

        Task SendCommentAsync(CommentBody commentBody, CancellationTokenSource cancellationTokenSource);

        Task<List<Comment>> GetPostCommentsAsync(string postId, CancellationTokenSource cancellationTokenSource);
    }
}
