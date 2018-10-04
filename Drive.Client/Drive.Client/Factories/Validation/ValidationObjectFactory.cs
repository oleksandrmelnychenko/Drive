using Drive.Client.Validations;

namespace Drive.Client.Factories.Validation {
    public class ValidationObjectFactory : IValidationObjectFactory {
        public ValidatableObject<T> GetValidatableObject<T>() {
            return new ValidatableObject<T>();
        }
    }
}
