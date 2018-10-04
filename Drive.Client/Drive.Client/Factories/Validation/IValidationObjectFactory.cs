using Drive.Client.Validations;

namespace Drive.Client.Factories.Validation {
    public interface IValidationObjectFactory {
        ValidatableObject<T> GetValidatableObject<T>();
    }
}
