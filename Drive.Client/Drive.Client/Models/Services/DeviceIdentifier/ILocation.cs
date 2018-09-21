using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.Models.Services.DeviceIdentifier {
    public interface ILocation {

        double Longitude { get; set; }

        double Latitude { get; set; }

        DateTime TimestampUtc { get; set; }
    }
}
