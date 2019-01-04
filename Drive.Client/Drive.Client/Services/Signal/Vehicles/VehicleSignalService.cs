using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.Services.Signal.Vehicles {
    public class VehicleSignalService : SignalBaseService, IVehicleSignalService {
        public override string SocketHubGateway { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

        protected override void OnStartListeningToHub() {
            throw new NotImplementedException();
        }
    }
}
