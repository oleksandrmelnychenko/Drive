using Drive.Client.Models.DataItems.Vehicle;
using Drive.Client.Models.EntityModels.Vehicle;
using System.Collections.Generic;

namespace Drive.Client.Models.Identities.NavigationArgs {
    public class VehicleArgs {
        public IEnumerable<VehicleDetail> VehicleDetails { get; set; }

        public ResidentRequestDataItem ResidentRequestDataItem { get; set; }
    }
}
