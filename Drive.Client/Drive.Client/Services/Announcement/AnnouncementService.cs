using Drive.Client.Exceptions;
using Drive.Client.Helpers;
using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.Models.Medias;
using Drive.Client.Models.Rest;
using Drive.Client.Services.Identity;
using Drive.Client.Services.RequestProvider;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.Announcement {
    public class AnnouncementService : IAnnouncementService {

        private readonly IRequestProvider _requestProvider;

        private readonly IIdentityService _identityService;

        /// <summary>
        ///     ctor().
        /// </summary>
        /// <param name="identityService"></param>
        /// <param name="requestProvider"></param>
        public AnnouncementService(IIdentityService identityService, IRequestProvider requestProvider) {
            _identityService = identityService;
            _requestProvider = requestProvider;
        }

        public Task DeleteAnnounceAsync(string id, CancellationTokenSource cancellationTokenSource) =>
             Task.Run(async () => {
                 try {
                     DrivenEvent announceActor = new DrivenEvent() {
                         Id = Guid.NewGuid().ToString(),
                         Data = id,
                         EventType = DrivenActorEvents.RemovePost,
                         UserNetId = BaseSingleton<GlobalSetting>.Instance.UserProfile.NetId
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

        public Task NewAnnouncementAsync(AnnounceBodyRequest announceBodyRequest, CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource), string eventId = "") =>
            Task.Run(async () => {
                try {
                    DrivenEvent announceActor = new DrivenEvent() {
                        Id = string.IsNullOrEmpty(eventId) ? Guid.NewGuid().ToString() : eventId,
                        Data = JsonConvert.SerializeObject(announceBodyRequest),
                        EventType = DrivenActorEvents.NewAnnounce,
                        UserNetId = BaseSingleton<GlobalSetting>.Instance.UserProfile.NetId
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

        public async Task<List<Announce>> GetAnnouncesAsync(CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource)) =>
            await Task.Run(async () => {
                List<Announce> announces = null;
                try {
                    string url = BaseSingleton<GlobalSetting>.Instance.RestEndpoints.AnnouncementEndPoints.GetAnnouncesEndpoint;
                    string accessToken = BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken;

                    announces = await _requestProvider.GetAsync<List<Announce>>(url, accessToken);
                }
                catch (ServiceAuthenticationException) {
                    await _identityService.LogOutAsync();
                }
                catch (Exception exc) {
                    Crashes.TrackError(exc);
                }
                return announces;
            }, cancellationTokenSource.Token);

        public Task AskToGetAnnouncementAsync(CancellationTokenSource cancellationTokenSource) =>
            Task.Run(async () => {
                try {
                    //DrivenEvent announceActor = new DrivenEvent() {
                    //    Id = Guid.NewGuid().ToString(),
                    //    EventType = DrivenActorEvents.GetAnnounces
                    //};

                    //string url = BaseSingleton<GlobalSetting>.Instance.RestEndpoints.AnnouncementEndPoints.NewAnnounce;
                    //string accessToken = BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken;

                    //await _requestProvider.PostAsync<object, DrivenEvent>(url, announceActor, accessToken);
                }
                catch (ServiceAuthenticationException) {
                    await _identityService.LogOutAsync();
                }
                catch (Exception exc) {
                    Crashes.TrackError(exc);
                }
            }, cancellationTokenSource.Token);

        public async Task<string> UploadAttachedDataAsync(IEnumerable<AttachedAnnounceMediaBase> attachedData, CancellationTokenSource cancellationTokenSource) =>
            await Task.Run(async () => {
                string eventId = Guid.NewGuid().ToString();

                try {
                    string url = string.Format(BaseSingleton<GlobalSetting>.Instance.RestEndpoints.AnnouncementEndPoints.UploadAttachedDataEndpoint,
                                               eventId,
                                               (int)DrivenActorEvents.NewAnnounce);
                    string accessToken = BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken;

                    object result = await _requestProvider.PostFormDataCollectionAsync<object, IEnumerable<MediaBase>>(url, attachedData, accessToken);
                    if (result != null) {
                        return eventId;
                    }
                }
                catch (ServiceAuthenticationException exc) {
                    await _identityService.LogOutAsync();
                    throw exc;
                }
                catch (Exception exc) {
                    Crashes.TrackError(exc);
                    throw exc;
                }

                return string.Empty;
            }, cancellationTokenSource.Token);

    }
}
