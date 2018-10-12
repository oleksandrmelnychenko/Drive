using Drive.Client.Helpers.Localize;

namespace Drive.Client.Validations {
    public interface IValidationRule<T> {
        StringResource ValidationMessage { get; set; }

        bool Check(T value);
    }
}
