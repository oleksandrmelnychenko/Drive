using Drive.Client.Factories.Validation;
using Drive.Client.Helpers.Localize;
using Drive.Client.Resources.Resx;
using Drive.Client.Validations;
using Drive.Client.Validations.ValidationRules;
using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.IdentityAccounting {
    public abstract class StepBaseViewModel : ContentPageBaseViewModel {

        public static readonly StringResource VIN_STEP_TITLE = ResourceLoader.Instance.GetString(nameof(AppStrings.InserVinUpperCase));
        public static readonly StringResource DATE_STEP_TITLE = ResourceLoader.Instance.GetString(nameof(AppStrings.InserDateUpperCase));
        public static readonly StringResource MIDDLENAME_STEP_REGISTRATION_TITLE = ResourceLoader.Instance.GetString(nameof(AppStrings.MiddleNameUpperCase));
        public static readonly StringResource MIDDLENAME_PLACEHOLDER_STEP_REGISTRATION = ResourceLoader.Instance.GetString(nameof(AppStrings.MiddleNameUpperCase));
        public static readonly StringResource PHONENUMBER_TITLE = ResourceLoader.Instance.GetString(nameof(AppStrings.PhoneNumberUpperCase));
        public static readonly StringResource CHANGE_PHONENUMBER_TITLE = ResourceLoader.Instance.GetString(nameof(AppStrings.ChangePhoneNumberUpperCase));
        public static readonly StringResource NAME_STEP_REGISTRATION_TITLE = ResourceLoader.Instance.GetString(nameof(AppStrings.EnterNameUpperCase));
        public static readonly StringResource CHANGE_EMAIL_TITLE = ResourceLoader.Instance.GetString(nameof(AppStrings.EmailUppercase));
        public static readonly StringResource PASSWORD_STEP_REGISTRATION_TITLE = ResourceLoader.Instance.GetString(nameof(AppStrings.EnterPasswordUpperCase));
        public static readonly StringResource CURRENT_PASSWORD_STEP_REGISTRATION_TITLE = ResourceLoader.Instance.GetString(nameof(AppStrings.EnterCurrentPasswordUpperCase));
        public static readonly StringResource NEW_PASSWORD_STEP_REGISTRATION_TITLE = ResourceLoader.Instance.GetString(nameof(AppStrings.EnterNewPasswordUpperCase));
        public static readonly StringResource PASSWORD_CONFIRM_STEP_REGISTRATION_TITLE = ResourceLoader.Instance.GetString(nameof(AppStrings.ConfirmPasswordUpperCase));
        public static readonly StringResource PHONE_PLACEHOLDER_STEP_REGISTRATION = ResourceLoader.Instance.GetString(nameof(AppStrings.PnoneNumberUpperCase));
        public static readonly StringResource NAME_PLACEHOLDER_STEP_REGISTRATION = ResourceLoader.Instance.GetString(nameof(AppStrings.NameUpperCase));
        public static readonly StringResource PASSWORD_PLACEHOLDER_STEP_REGISTRATION = ResourceLoader.Instance.GetString(nameof(AppStrings.PasswordUpperCase));

        public static readonly string PHONENUMBER_ICON_PATH = "resource://Drive.Client.Resources.Images.Phone.svg";
        public static readonly string NAME_ICON_PATH = "resource://Drive.Client.Resources.Images.name.svg";
        public static readonly string EMAIL_ICON_PATH = "resource://Drive.Client.Resources.Images.Email.svg";
        public static readonly string PASSWORD_ICON_PATH = "resource://Drive.Client.Resources.Images.password.svg";
        public static readonly string DATE_ICON_PATH = "resource://Drive.Client.Resources.Images.Calendar.svg";
        public static readonly string TODO_INPUT_ICON_STUB = "todo: appropriate icon path";

        private readonly IValidationObjectFactory _validationObjectFactory;

        /// <summary>
        ///     ctor().
        /// </summary>
        public StepBaseViewModel() {
            _validationObjectFactory = DependencyLocator.Resolve<IValidationObjectFactory>();

            ActionBarViewModel = DependencyLocator.Resolve<IdentityAccountingActionBarViewModel>();
            ((IdentityAccountingActionBarViewModel)ActionBarViewModel).InitializeAsync(this);

            _mainInput = _validationObjectFactory.GetValidatableObject<string>();

            ResetValidationObjects();
        }

        public ICommand StepCommand => new Command(() => OnStepCommand());

        public ICommand CleanServerErrorCommand => new Command(() => ServerError = string.Empty);

        string _serverError;
        public string ServerError {
            get { return _serverError; }
            set { SetProperty(ref _serverError, value); }
        }

        StringResource _stepTitle;
        public StringResource StepTitle {
            get => _stepTitle;
            protected set => SetProperty(ref _stepTitle, value);
        }

        string _mainInputIconPath;
        public string MainInputIconPath {
            get => _mainInputIconPath;
            protected set => SetProperty(ref _mainInputIconPath, value);
        }

        StringResource _mainInputPlaceholder;
        public StringResource MainInputPlaceholder {
            get => _mainInputPlaceholder;
            protected set => SetProperty(ref _mainInputPlaceholder, value);
        }

        Keyboard _keyboardType = Keyboard.Default;
        public Keyboard KeyboardType {
            get => _keyboardType;
            protected set => SetProperty(ref _keyboardType, value);
        }

        bool _isPasswordInput;
        public bool IsPasswordInput {
            get => _isPasswordInput;
            protected set => SetProperty(ref _isPasswordInput, value);
        }

        ValidatableObject<string> _mainInput;
        public ValidatableObject<string> MainInput {
            get => _mainInput;
            private set => SetProperty(ref _mainInput, value);
        }

        public virtual bool ValidateForm() {
            bool isValidResult = false;

            MainInput.Validate();

            isValidResult = MainInput.IsValid;

            return isValidResult;
        }

        public virtual void ResetInputForm() {
            ResetValidationObjects();
        }

        protected virtual void ResetValidationObjects() {
            MainInput = _validationObjectFactory.GetValidatableObject<string>();
            MainInput.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = ValidatableObject<string>.FIELD_IS_REQUIRED_VALIDATION_MESSAGE });
        }

        protected abstract void OnStepCommand();
    }
}
