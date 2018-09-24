using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.Validations.ValidationRules {
    public class LengthRule<T> :IValidationRule<T> {
        public string ValidationMessage { get; set; }

        public bool Check(T value) {
            if (value == null)
                return false;

            string validatedValue = value as string;

            //bool res = !();

            return validatedValue.Length < 4;
        }
    }
}
