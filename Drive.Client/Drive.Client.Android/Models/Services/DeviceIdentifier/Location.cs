using Drive.Client.Models.Services.DeviceIdentifier;
using System;

namespace Drive.Client.Droid.Models.Services.DeviceIdentifier {
    public class Location : ILocation {

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public DateTime TimestampUtc { get; set; }
    }
}