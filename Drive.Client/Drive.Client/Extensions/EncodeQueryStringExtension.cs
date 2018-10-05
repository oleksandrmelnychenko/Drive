using System;

namespace Drive.Client.Extensions {
    public static class EncodeQueryStringExtension {
        public static string EncodeQueryString(this string value) {
            return Uri.EscapeDataString(value);
        }
    }
}
