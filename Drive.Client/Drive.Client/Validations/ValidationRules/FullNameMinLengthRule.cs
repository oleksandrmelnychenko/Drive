using Drive.Client.Helpers.Localize;

namespace Drive.Client.Validations.ValidationRules {
    public class FullNameMinLengthRule<T> : IValidationRule<T> {
        public StringResource ValidationMessage { get; set; }

        public bool Check(T value) {
            if (value == null)
                return false;

            string validatedValue = value as string;

            if (validatedValue.Length < 2) {
                return false;
            }

            return true;
        }
    }
}
