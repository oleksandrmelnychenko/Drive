using Drive.Client.Helpers;
using Microsoft.AppCenter.Crashes;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Diagnostics;

namespace Drive.Client.Services.Signal.Announcement {
    public class AnnouncementHubService : SignalBaseService, IAnnouncementHubService {

        private static readonly string _NEW_ANNOUNCE = "NewAnnounceHubEndpoint";

        public event EventHandler<object> NewAnnounceReceived = delegate { };

        public override string SocketHubGateway { get; protected set; } = BaseSingleton<GlobalSetting>.Instance.RestEndpoints.SignalGateways.Announcements;

        protected override void OnStartListeningToHub() {
            _hubConnection.On<object>(_NEW_ANNOUNCE, (args) => {
                try {
                    Console.WriteLine("===> AnnouncementHubService.NewAnnounceHubEndpoint <===");
                    ///
                    /// TODO: parse parameter. Check if logged in andinvoke event.
                    ///
                    //NewAnnounceReceived.Invoke(this, args);
                }
                catch (Exception exc) {
                    Crashes.TrackError(exc);
                    Debugger.Break();
                }
            });
        }
    }
}
    