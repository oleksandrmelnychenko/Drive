using System;

namespace Drive.Client.Exceptions {
    internal class ConnectivityException : Exception {
        public ConnectivityException(string error) : base(error) {
        }
    }
}
