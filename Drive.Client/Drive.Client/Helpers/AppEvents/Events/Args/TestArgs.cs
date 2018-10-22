using Drive.Client.Models.EntityModels.Search;
using System;

namespace Drive.Client.Helpers.AppEvents.Events.Args {
    public class TestArgs : EventArgs {
        public VehicleDetailsByResidentFullName VehicleDetailsByResidentFullName { get; set; }
    }
}
