using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Drive.Client.Validations.ValidationRules {
    class PhoneNumberRule<T> : IValidationRule<T> {

        public string PhonePattern { get; set; } = "+X X(XXX) XX-XXX-XX";

        public string ValidationMessage { get; set; }

        public bool Check(T value) {

            if ((value is string)) {
                string valueString = value as string;

                if (valueString.Count() == PhonePattern.Count()) {
                    for (int i = 0; i < PhonePattern.Count(); i++) {
                        if (PhonePattern[i] == 'X' && !char.IsDigit(valueString[i])) {
                            return false;
                        }
                    }
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return value != null;
            }
        }
    }
}
