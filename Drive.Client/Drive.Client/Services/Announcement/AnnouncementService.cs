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

        public Task<bool> NewAnnouncementAsync(TODOAnnounce announce, CancellationTokenSource cancellationTokenSource) =>
            Task<bool>.Run(async () => {
                bool completion = false;

                try {
                    Actor announceActor = new Actor() {
                        Id = Guid.NewGuid().ToString(),
                        Data = JsonConvert.SerializeObject(announce),
                        EventType = DrivenActorEvents.NewAnnounce,
                        UserNetId = BaseSingleton<GlobalSetting>.Instance.UserProfile.NetId
                    };

                    await _requestProvider.PostAsync<object, Actor>(BaseSingleton<GlobalSetting>.Instance.RestEndpoints.AnnouncementEndPoints.NewAnnounce,
                        announceActor,
                        BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken);

                    completion = true;
                }
                catch (ServiceAuthenticationException exc) {
                    completion = false;

                    await _identityService.LogOutAsync();
                    throw exc;
                }
                catch (Exception exc) {
                    completion = false;

                    Crashes.TrackError(exc);
                    throw exc;
                }

                return completion;
            }, cancellationTokenSource.Token);
    }
}
