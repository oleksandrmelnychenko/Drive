using Drive.Client.Helpers.Localize;
using Drive.Client.Resources.Resx;
using Drive.Client.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;

namespace Drive.Client.Validations {
    public class ValidatableObject<T> : ExtendedBindableObject, IValidity {

        public static readonly StringResource FIELD_IS_REQUIRED_VALIDATION_MESSAGE =
            ResourceLoader.Instance.GetString(nameof(AppStrings.FieldRequired));
        
        public static readonly StringResource INVALID_PHONE_VALIDATION_MESSAGE =
            ResourceLoader.Instance.GetString(nameof(AppStrings.InvalidPhoneNumber));
        
        public static readonly StringResource INVALID_DATE_FORMAT_VALIDATION_MESSAGE =
            ResourceLoader.Instance.GetString(nameof(AppStrings.IncorrectDateFormat));

        public static readonly StringResource ERROR_EMAIL = ResourceLoader.Instance.GetString(nameof(AppStrings.InvalidEmail));

        public static readonly StringResource ERROR_MINLENGTH = ResourceLoader.Instance.GetString(nameof(AppStrings.ERROR_MINLENGTH));

        public static readonly StringResource ERROR_MAXLENGTH = ResourceLoader.Instance.GetString(nameof(AppStrings.ERROR_MAXLENGTH));


        public List<IValidationRule<T>> Validations { get; }

        public ValidatableObject() {
            _isValid = true;
            _errors = new List<StringResource>();
            Validations = new List<IValidationRule<T>>();
        }

        List<StringResource> _errors;
        public List<StringResource> Errors {
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

            IEnumerable<StringResource> errors = Validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }
    }
}
