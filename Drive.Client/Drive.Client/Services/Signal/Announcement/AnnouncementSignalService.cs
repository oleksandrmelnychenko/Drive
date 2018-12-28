using Drive.Client.Helpers;
using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.Models.EntityModels.Announcement.Comments;
using Drive.Client.Models.Rest;
using Drive.Client.Services.Identity;
using Drive.Client.ViewModels.Base;
using Microsoft.AppCenter.Crashes;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace Drive.Client.Services.Signal.Announcement {
    public class AnnouncementSignalService : SignalBaseService, IAnnouncementSignalService {

        private const string NEW_ANNOUNCE = "NewPostHubEndpoint";

        private const string NEW_POST_COMMENT = "NewPostCommentHubEndpoint";

        private const string UPDATE_POST_COMMENTS_C0UNT = "UpdatePostCommentsCountHubEndpoint";

        private const string REMOVE_POST = "RemovePostHubEndpoint";

        private const string UPDATE_POST_LIKES_COUNT = "UpdatePostLikesCountHubEndpoint";

        public event EventHandler<Announce> NewAnnounceReceived = delegate { };

        public event EventHandler<CommentCountBody> PostCommentsCountReceived = delegate { };

        public event EventHandler<Comment> NewPostCommentReceived = delegate { };

        public event EventHandler<string> DeletedPostReceived = delegate { };

        public event EventHandler<PostLikedBody> PostLikesCountReceived = delegate { };

        public override string SocketHubGateway { get; protected set; } = BaseSingleton<GlobalSetting>.Instance.RestEndpoints.SignalGateways.Announcements;

        protected override void OnStartListeningToHub() {
            _hubConnection.On<object>(UPDATE_POST_LIKES_COUNT, async (args) =>
            {
                try
                {
                    DrivenEventResponse drivenEventResponse = ParseResponseData<DrivenEventResponse>(args);

                    if (drivenEventResponse.StatusCode == HttpStatusCode.OK)
                    {
                        DrivenEvent drivenEvent = ParseResponseData<DrivenEvent>(drivenEventResponse.Data);

                        PostLikedBody postLikedBody = ParseResponseData<PostLikedBody>(drivenEvent.Data);
                        PostLikesCountReceived(this, postLikedBody);
                    }
                    else if (drivenEventResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        await DependencyLocator.Resolve<IIdentityService>().LogOutAsync();
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
            });

            _hubConnection.On<object>(REMOVE_POST, async (args) =>
            {
                try
                {
                    DrivenEventResponse drivenEventResponse = ParseResponseData<DrivenEventResponse>(args);

                    if (drivenEventResponse.StatusCode == HttpStatusCode.OK)
                    {
                        DrivenEvent drivenEvent = ParseResponseData<DrivenEvent>(drivenEventResponse.Data);

                        string postId = ParseResponseData<string>(drivenEvent.Data);
                        DeletedPostReceived(this, postId);
                    }
                    else if (drivenEventResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        await DependencyLocator.Resolve<IIdentityService>().LogOutAsync();
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
            });

            _hubConnection.On<object>(NEW_POST_COMMENT, async (args) =>
            {
                try
                {
                    DrivenEventResponse drivenEventResponse = ParseResponseData<DrivenEventResponse>(args);

                    if (drivenEventResponse.StatusCode == HttpStatusCode.OK)
                    {
                        DrivenEvent drivenEvent = ParseResponseData<DrivenEvent>(drivenEventResponse.Data);

                        Comment comment = ParseResponseData<Comment>(drivenEvent.Data);
                        NewPostCommentReceived(this, comment);
                    }
                    else if (drivenEventResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        await DependencyLocator.Resolve<IIdentityService>().LogOutAsync();
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
            });

            _hubConnection.On<object>(UPDATE_POST_COMMENTS_C0UNT, async (args) =>
            {
                try
                {
                    DrivenEventResponse drivenEventResponse = ParseResponseData<DrivenEventResponse>(args);

                    if (drivenEventResponse.StatusCode == HttpStatusCode.OK)
                    {
                        DrivenEvent drivenEvent = ParseResponseData<DrivenEvent>(drivenEventResponse.Data);

                        CommentCountBody commentCountBody = ParseResponseData<CommentCountBody>(drivenEvent.Data);
                        PostCommentsCountReceived.Invoke(this, commentCountBody);
                    }
                    else if (drivenEventResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        await DependencyLocator.Resolve<IIdentityService>().LogOutAsync();
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
            });

            _hubConnection.On<object>(NEW_ANNOUNCE, async (args) =>
            {
                try
                {
                    Console.WriteLine("===> AnnouncementHubService.NewPostHubEndpoint <===");
                    DrivenEventResponse drivenEventResponse = ParseResponseData<DrivenEventResponse>(args);

                    if (drivenEventResponse.StatusCode == HttpStatusCode.OK)
                    {
                        DrivenEvent drivenEvent = ParseResponseData<DrivenEvent>(drivenEventResponse.Data);

                        Announce announce = ParseResponseData<Announce>(drivenEvent.Data);
                        if (announce != null && announce.AnnounceBody != null && announce.ImageUrl != null)
                        {
                            NewAnnounceReceived.Invoke(this, announce);
                        }
                        else
                        {
                            announce = new Announce();
                            AnnounceBody announceBody = ParseResponseData<AnnounceBody>(drivenEvent.Data);
                            announce.AnnounceBody = announceBody;
                            NewAnnounceReceived.Invoke(this, announce);
                        }
                    }
                    else if (drivenEventResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        await DependencyLocator.Resolve<IIdentityService>().LogOutAsync();
                    }
                }
                catch (Exception exc)
                {
                    Crashes.TrackError(exc);
                    Debugger.Break();
                }
            });
        }
    }
}
