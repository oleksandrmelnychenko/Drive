using Drive.Client.Exceptions;
using Drive.Client.Helpers;
using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.Models.Rest;
using Drive.Client.Services.Identity;
using Drive.Client.Services.RequestProvider;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.Announcement {
    public class AnnouncementService : IAnnouncementService {

        private readonly IRequestProvider _requestProvider;
        private readonly IIdentityService _identityService;

        public AnnouncementService(
            IIdentityService identityService,
            IRequestProvider requestProvider) {

            _identityService = identityService;
            _requestProvider = requestProvider;
        }

        public Task NewAnnouncementAsync(AnnounceBody announce, CancellationTokenSource cancellationTokenSource) =>
            Task.Run(async () => {
                try {
                    DrivenEvent announceActor = new DrivenEvent() {
                        Id = Guid.NewGuid().ToString(),
                        Data = JsonConvert.SerializeObject(announce),
                        EventType = DrivenActorEvents.NewAnnounce,
                        UserNetId = BaseSingleton<GlobalSetting>.Instance.UserProfile.NetId
                    };

                    await _requestProvider.PostAsync<object, DrivenEvent>(BaseSingleton<GlobalSetting>.Instance.RestEndpoints.AnnouncementEndPoints.NewAnnounce,
                        announceActor,
                        BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken);

                }
                catch (ServiceAuthenticationException exc) {

                    await _identityService.LogOutAsync();
                    throw exc;
                }
                catch (Exception exc) {

                    Crashes.TrackError(exc);
                    throw exc;
                }

            }, cancellationTokenSource.Token);

        public Task AskToGetAnnouncementAsync(CancellationTokenSource cancellationTokenSource) =>
            Task<bool>.Run(async () => {
                try {
                    DrivenEvent announceActor = new DrivenEvent() {
                        Id = Guid.NewGuid().ToString(),
                        EventType = DrivenActorEvents.GetAnnounces
                    };

                    await _requestProvider.PostAsync<object, DrivenEvent>(BaseSingleton<GlobalSetting>.Instance.RestEndpoints.AnnouncementEndPoints.NewAnnounce,
                        announceActor,
                        BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken);
                }
                catch (ServiceAuthenticationException exc) {
                    await _identityService.LogOutAsync();
                    throw exc;
                }
                catch (Exception exc) {
                    Crashes.TrackError(exc);
                    throw exc;
                }
            }, cancellationTokenSource.Token);
    }
}
