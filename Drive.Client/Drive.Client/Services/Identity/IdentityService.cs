using Drive.Client.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.Services.Identity {
    public class IdentityService : IIdentityService {

        private readonly IRequestProvider _requestProvider;

        /// <summary>
        ///     ctor().
        /// </summary>
        public IdentityService(IRequestProvider requestProvider) {
            _requestProvider = requestProvider;
        }
    }
}
