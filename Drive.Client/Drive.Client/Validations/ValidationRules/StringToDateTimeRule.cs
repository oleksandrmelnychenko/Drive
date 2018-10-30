using Drive.Client.Helpers.Localize;
using System;

namespace Drive.Client.Validations.ValidationRules {
    public class StringToDateTimeRule<T> : IValidationRule<T> {
        public StringResource ValidationMessage { get; set; }

        public bool Check(T value) {
            if ((value is string)) {
                string valueString = value as string;

                DateTime dateTime = new DateTime();
                
                return DateTime.TryParse(valueString, out dateTime);
            }
            else {
                return value != null;
            }
        }
    }
}
