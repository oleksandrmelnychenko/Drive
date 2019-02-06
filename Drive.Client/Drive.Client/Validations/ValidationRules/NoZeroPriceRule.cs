using Drive.Client.Helpers.Localize;
using System;

namespace Drive.Client.Validations.ValidationRules {
    internal class NoZeroPriceRule<T> : IValidationRule<T> {

        public StringResource ValidationMessage { get; set; }

        public bool Check(T value) {
            try {
                if (value is decimal) {
                    var validatedValue = Convert.ToDecimal(value);

                    if (validatedValue <= 0) {
                        return false;
                    } else {
                        return true;
                    }
                }
            }
            catch (Exception) {
                return false;
            }
            return false;
        }
    }
}
