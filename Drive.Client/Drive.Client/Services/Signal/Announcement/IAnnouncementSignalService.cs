using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.Models.EntityModels.Announcement.Comments;
using System;

namespace Drive.Client.Services.Signal.Announcement {
    public interface IAnnouncementSignalService : ISignalService {
        event EventHandler<Announce> NewAnnounceReceived;

        event EventHandler<CommentCountBody> PostCommentsCountReceived;

        event EventHandler<Comment> NewPostCommentReceived;

        event EventHandler<string> DeletedPostReceived;

        event EventHandler<PostLikedBody> PostLikesCountReceived;
    }
}
