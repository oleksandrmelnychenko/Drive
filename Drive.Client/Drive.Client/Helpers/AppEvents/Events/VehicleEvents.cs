using System;

namespace Drive.Client.Helpers.AppEvents.Events {
    public class VehicleEvents {
        public event EventHandler SendInformation = delegate { };

        public void OnSendInformation() => SendInformation(this, new EventArgs());
    }
}
