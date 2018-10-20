using Drive.Client.Helpers.Localize;

namespace Drive.Client.Validations.ValidationRules {
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T> {

        public StringResource ValidationMessage { get; set; }

        public bool Check(T value) {

            if ((value is string)) {
                var validatedValue = value as string;

                return (!string.IsNullOrWhiteSpace(validatedValue) && !string.IsNullOrEmpty(validatedValue));
            }
            else {
                return value != null;
            }
        }
    }   
}
