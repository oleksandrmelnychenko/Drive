using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Drive.Client.Helpers {
    public static class ValidationHelper {
        public const string EMAIL = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public const string ALPHANUMERIC_SYMBOLS = @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]";

        public const string ALLOW_SOME_SYMBOLS = @"^[a-zA-Z\s\-]*$";

        public const string US_PHONE_NUMBER = @"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$";

        private const int MINIMUM_LENGTH = 6;

        private const string VALID_DATE = @"^(3[01]|[12][0-9]|0[1-9])\/(1[012]|0[1-9])\/((?:19|20)\d{2})$";

        public static bool IsValidDate(string value) {
            if (string.IsNullOrEmpty(value)) {
                return false;
            }

            var regex = new Regex(VALID_DATE);
            return regex.IsMatch(value);
        }

        public static bool IsUSPhoneNumber(string value) {
            if (string.IsNullOrEmpty(value)) {
                return false;
            }

            var regex = new Regex(US_PHONE_NUMBER);
            return regex.IsMatch(value);
        }

        public static bool IsEmail(string value) {
            if (string.IsNullOrEmpty(value)) {
                return false;
            }

            var regex = new Regex(EMAIL);
            return regex.IsMatch(value);
        }

        public static bool HasDigit(string value) {
            bool hasDigit = false;
            foreach (var c in value) {
                if (char.IsDigit(c)) {
                    hasDigit = true;
                }
            }
            return hasDigit;
        }

        public static bool HasUpperCase(string value) {
            bool hasUpperCase = false;
            foreach (var c in value) {
                if (char.IsUpper(c)) {
                    hasUpperCase = true;
                }
            }
            return hasUpperCase;
        }

        public static bool HasSymbols(string value) {
            if (string.IsNullOrEmpty(value)) {
                return false;
            }

            var regex = new Regex(ALPHANUMERIC_SYMBOLS);
            return regex.IsMatch(value);
        }

        public static bool HasSomeSymbols(string value) {
            if (string.IsNullOrEmpty(value)) {
                return false;
            }

            var regex = new Regex(ALLOW_SOME_SYMBOLS);
            return regex.IsMatch(value);
        }

        public static bool MinLengthPassword(string value) {
            if (string.IsNullOrEmpty(value))
                return false;

            bool result = false;
            if (value.Length >= MINIMUM_LENGTH)
                result = true;
            return result;
        }
    }
}
