﻿using System;
using Xamarin.Forms;

namespace Drive.Client.Services.OpenUrl {
    public class OpenUrlService : IOpenUrlService {
        public void OpenUrl(string url) {
            Device.OpenUri(new Uri(url));
        }
    }
}
