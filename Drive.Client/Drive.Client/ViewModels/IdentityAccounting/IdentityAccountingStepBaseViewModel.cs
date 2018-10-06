using Drive.Client.Factories.Validation;
using Drive.Client.Models.Arguments.IdentityAccounting.Registration;
using Drive.Client.Validations;
using Drive.Client.Validations.ValidationRules;
using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.IdentityAccounting {
    public abstract class IdentityAccountingStepBaseViewModel : ContentPageBaseViewModel {

        public static readonly string PHONE_STEP_REGISTRATION_TITLE = "ВВЕДІТЬ НОМЕР ТЕЛЕФОНУ";
        public static readonly string CHANGE_PHONENUMBER_TITLE = "ЗМІНИТИ НОМЕР ТЕЛЕФОНУ";
        public static readonly string PHONE_STEP_SIGNIN_TITLE = "ВВЕДІТЬ ВАШ НОМЕР ТЕЛЕФОНУ";
        public static readonly string NAME_STEP_REGISTRATION_TITLE = "ВВЕДІТЬ ІМ'Я";
        public static readonly string PASSWORD_STEP_REGISTRATION_TITLE = "ВВЕДІТЬ ПАРОЛЬ";
        public static readonly string PASSWORD_CONFIRM_STEP_REGISTRATION_TITLE = "ПІДТВЕРДІТЬ ПАРОЛЬ";

        public static readonly string PHONE_PLACEHOLDER_STEP_REGISTRATION = "ТЕЛЕФОН";
        public static readonly string NAME_PLACEHOLDER_STEP_REGISTRATION = "ІМ'Я";
        public static readonly string PASSWORD_PLACEHOLDER_STEP_REGISTRATION = "ПАРОЛЬ";

        public static readonly string PHONENUMBER_ICON_PATH = "resource://Drive.Client.Resources.Images.Phone.svg";
        public static readonly string NAME_ICON_PATH = "resource://Drive.Client.Resources.Images.name.svg";
        public static readonly string PASSWORD_ICON_PATH = "resource://Drive.Client.Resources.Images.password.svg";
        public static readonly string TODO_INPUT_ICON_STUB = "todo: appropriate icon path";

        private readonly IValidationObjectFactory _validationObjectFactory;

        //public static readonly string TODO_INPUT_ICON_STUB = "todo: appropriate icon path";

        public IdentityAccountingStepBaseViewModel() {
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

        string _stepTitle;
        public string StepTitle {
            get => _stepTitle;
            protected set => SetProperty(ref _stepTitle, value);
        }

        string _mainInputIconPath;
        public string MainInputIconPath {
            get => _mainInputIconPath;
            protected set => SetProperty(ref _mainInputIconPath, value);
        }

        string _mainInputPlaceholder;
        public string MainInputPlaceholder {
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
