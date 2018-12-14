using Drive.Client.Helpers;
using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.Models.Rest;
using Drive.Client.Services.Identity;
using Drive.Client.ViewModels.Base;
using Microsoft.AppCenter.Crashes;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Diagnostics;
using System.Net;

namespace Drive.Client.Services.Signal.Announcement {
    public class AnnouncementSignalService : SignalBaseService, IAnnouncementSignalService {

        private static readonly string _NEW_ANNOUNCE = "NewPostHubEndpoint";
        private static readonly string _GET_ANNOUNCEMENT = "GetPostHubEndpoint";

        public event EventHandler<Announce> NewAnnounceReceived = delegate { };
        public event EventHandler<Announce[]> GetAnnouncement = delegate { };

        public override string SocketHubGateway { get; protected set; } = BaseSingleton<GlobalSetting>.Instance.RestEndpoints.SignalGateways.Announcements;

        protected override void OnStartListeningToHub() {
            _hubConnection.On<object>(_NEW_ANNOUNCE, async (args) => {
                try {
                    Console.WriteLine("===> AnnouncementHubService.NewPostHubEndpoint <===");
                    DrivenEventResponse drivenEventResponse = ParseResponseData<DrivenEventResponse>(args);

                    if (drivenEventResponse.StatusCode == HttpStatusCode.OK) {
                        DrivenEvent drivenEvent = ParseResponseData<DrivenEvent>(drivenEventResponse.Data);

                        ///
                        /// TODO
                        /// 
                        //Announce announce = ParseResponseData<Announce>(drivenEvent.Data);
                        //AnnounceBody announceBody = ParseResponseData<AnnounceBody>(drivenEvent.Data);
                        //NewAnnounceReceived.Invoke(this, );
                    }
                    else if (drivenEventResponse.StatusCode == HttpStatusCode.Unauthorized) {
                        await DependencyLocator.Resolve<IIdentityService>().LogOutAsync();
                    }
                }
                catch (Exception exc) {
                    Crashes.TrackError(exc);
                    Debugger.Break();
                }
            });

            _hubConnection.On<object>(_GET_ANNOUNCEMENT, async (args) => {
                try {
                    Console.WriteLine("===> AnnouncementHubService.GetPostHubEndpoint <===");
                    DrivenEventResponse drivenEventResponse = ParseResponseData<DrivenEventResponse>(args);

                    if (drivenEventResponse.StatusCode == HttpStatusCode.OK) {
                        DrivenEvent drivenEvent = ParseResponseData<DrivenEvent>(drivenEventResponse.Data);

                        Announce[] announcement = ParseResponseData<Announce[]>(drivenEvent.Data);
                        GetAnnouncement.Invoke(this, announcement);
                    }
                    else if (drivenEventResponse.StatusCode == HttpStatusCode.Unauthorized) {
                        await DependencyLocator.Resolve<IIdentityService>().LogOutAsync();
                    }
                }
                catch (Exception exc) {
                    Crashes.TrackError(exc);
                    Debugger.Break();
                }
            });
        }
    }
}
