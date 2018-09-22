using Drive.Client.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;

namespace Drive.Client.Validations {
    public class ValidatableObject<T> : ExtendedBindableObject, IValidity {

        public static readonly string FIELD_IS_REQUIRED_VALIDATION_MESSAGE = "This field is required";
        public static readonly string INVALID_PHONE_VALIDATION_MESSAGE = "Invalid phone number";

        public List<IValidationRule<T>> Validations { get; }

        public ValidatableObject() {
            _isValid = true;
            _errors = new List<string>();
            Validations = new List<IValidationRule<T>>();
        }

        List<string> _errors;
        public List<string> Errors {
            get { return _errors; }
            set { SetProperty(ref _errors, value); }
        }

        T _value;
        public T Value {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

        bool _isValid;
        public bool IsValid {
            get { return _isValid; }
            set { SetProperty(ref _isValid, value); }
        }

        public bool Validate() {
            Errors.Clear();

            IEnumerable<string> errors = Validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }
    }
}
