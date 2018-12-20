using Drive.Client.Exceptions;
using Drive.Client.Helpers;
using Drive.Client.Models.EntityModels.Announcement.Comments;
using Drive.Client.Models.Rest;
using Drive.Client.Services.Identity;
using Drive.Client.Services.RequestProvider;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.Comments {
    public class CommentService : ICommentService {

        private readonly IRequestProvider _requestProvider;

        private readonly IIdentityService _identityService;

        /// <summary>
        /// ctor().
        /// </summary>
        public CommentService(IIdentityService identityService, IRequestProvider requestProvider) {
            _identityService = identityService;
            _requestProvider = requestProvider;
        }

        public Task GetPostCommentsById(string postId, CancellationTokenSource cancellationTokenSource) =>
            Task.Run(async () => {
                try {
                    DrivenEvent announceActor = new DrivenEvent() {
                        Id = Guid.NewGuid().ToString(),
                        EventType = DrivenActorEvents.GetPostComments,
                        Data = postId
                    };

                    string url = BaseSingleton<GlobalSetting>.Instance.RestEndpoints.AnnouncementEndPoints.NewAnnounce;
                    string accessToken = BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken;

                    await _requestProvider.PostAsync<object, DrivenEvent>(url, announceActor, accessToken);
                }
                catch (ServiceAuthenticationException) {
                    await _identityService.LogOutAsync();
                }
                catch (Exception exc) {
                    Crashes.TrackError(exc);
                }
            }, cancellationTokenSource.Token);

        public Task SendCommentAsync(CommentBody commentBody, CancellationTokenSource cancellationTokenSource)=>
            Task.Run(async () => {
                try {
                    DrivenEvent announceActor = new DrivenEvent() {
                        Id = Guid.NewGuid().ToString(),
                        EventType = DrivenActorEvents.CommentPost,
                        Data = JsonConvert.SerializeObject(commentBody),
                    };

                    string url = BaseSingleton<GlobalSetting>.Instance.RestEndpoints.AnnouncementEndPoints.NewAnnounce;
                    string accessToken = BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken;

                    await _requestProvider.PostAsync<object, DrivenEvent>(url, announceActor, accessToken);
                }
                catch (ServiceAuthenticationException) {
                    await _identityService.LogOutAsync();
                }
                catch (Exception exc) {
                    Crashes.TrackError(exc);
                }
            }, cancellationTokenSource.Token);
    }
}
