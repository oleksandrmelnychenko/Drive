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

        public static StringResource VIN_STEP_TITLE;
        public static StringResource FIRST_DATE_REGISTRATION_TITLE;
        public static StringResource DATE_STEP_TITLE;
        public static StringResource DATE_TITLE;
        public static StringResource MIDDLENAME_STEP_REGISTRATION_TITLE;
        public static StringResource MIDDLENAME_PLACEHOLDER_STEP_REGISTRATION;
        public static StringResource PHONENUMBER_TITLE;
        public static StringResource CHANGE_PHONENUMBER_TITLE;
        public static StringResource NAME_STEP_REGISTRATION_TITLE;
        public static StringResource CHANGE_EMAIL_TITLE;
        public static StringResource PASSWORD_STEP_REGISTRATION_TITLE;
        public static StringResource CURRENT_PASSWORD_STEP_REGISTRATION_TITLE;
        public static StringResource NEW_PASSWORD_STEP_REGISTRATION_TITLE;
        public static StringResource PASSWORD_CONFIRM_STEP_REGISTRATION_TITLE;
        public static StringResource PHONE_PLACEHOLDER_STEP_REGISTRATION;
        public static StringResource NAME_PLACEHOLDER_STEP_REGISTRATION;
        public static StringResource PASSWORD_PLACEHOLDER_STEP_REGISTRATION;
        public static StringResource VEHICLE_VIN_CODE_PLACEHOLDER_STEP;
        public static StringResource REGISTRATION_DATE_PLACEHOLDER_STEP;
        //public static readonly StringResource INVALID_PASSWORD_WARNING = ResourceLoader.Instance.GetString(nameof(AppStrings.InvalidPassword));

        public static readonly string PHONENUMBER_ICON_PATH = "resource://Drive.Client.Resources.Images.Phone.svg";
        public static readonly string NAME_ICON_PATH = "resource://Drive.Client.Resources.Images.name.svg";
        public static readonly string EMAIL_ICON_PATH = "resource://Drive.Client.Resources.Images.Email.svg";
        public static readonly string PASSWORD_ICON_PATH = "resource://Drive.Client.Resources.Images.password.svg";
        public static readonly string DATE_ICON_PATH = "resource://Drive.Client.Resources.Images.Calendar.svg";
        public static readonly string VEHICLE_VIN_CODE_ICON_PATH = "resource://Drive.Client.Resources.Images.ic_vin.svg";
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

        //Keyboard _keyboardType = Keyboard.Default;
        //public Keyboard KeyboardType {
        //    get => _keyboardType;
        //    protected set => SetProperty(ref _keyboardType, value);
        //}

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

        protected override void ResolveStringResources() {
            base.ResolveStringResources();

            VIN_STEP_TITLE = ResourceLoader.GetString(nameof(AppStrings.InserVinUpperCase));
            FIRST_DATE_REGISTRATION_TITLE = ResourceLoader.GetString(nameof(AppStrings.FirstRegistrationDateUppercase));
            DATE_STEP_TITLE = ResourceLoader.GetString(nameof(AppStrings.InserDateUpperCase));
            DATE_TITLE = ResourceLoader.GetString(nameof(AppStrings.DateUpperCase));
            MIDDLENAME_STEP_REGISTRATION_TITLE = ResourceLoader.GetString(nameof(AppStrings.MiddleNameUpperCase));
            MIDDLENAME_PLACEHOLDER_STEP_REGISTRATION = ResourceLoader.GetString(nameof(AppStrings.MiddleNameUpperCase));
            PHONENUMBER_TITLE = ResourceLoader.GetString(nameof(AppStrings.PhoneNumberUpperCase));
            CHANGE_PHONENUMBER_TITLE = ResourceLoader.GetString(nameof(AppStrings.ChangePhoneNumberUpperCase));
            NAME_STEP_REGISTRATION_TITLE = ResourceLoader.GetString(nameof(AppStrings.EnterNameUpperCase));
            CHANGE_EMAIL_TITLE = ResourceLoader.GetString(nameof(AppStrings.EmailUppercase));
            PASSWORD_STEP_REGISTRATION_TITLE = ResourceLoader.GetString(nameof(AppStrings.EnterPasswordUpperCase));
            CURRENT_PASSWORD_STEP_REGISTRATION_TITLE = ResourceLoader.GetString(nameof(AppStrings.EnterCurrentPasswordUpperCase));
            NEW_PASSWORD_STEP_REGISTRATION_TITLE = ResourceLoader.GetString(nameof(AppStrings.EnterNewPasswordUpperCase));
            PASSWORD_CONFIRM_STEP_REGISTRATION_TITLE = ResourceLoader.GetString(nameof(AppStrings.ConfirmPasswordUpperCase));
            PHONE_PLACEHOLDER_STEP_REGISTRATION = ResourceLoader.GetString(nameof(AppStrings.PnoneNumberUpperCase));
            NAME_PLACEHOLDER_STEP_REGISTRATION = ResourceLoader.GetString(nameof(AppStrings.NameUpperCase));
            PASSWORD_PLACEHOLDER_STEP_REGISTRATION = ResourceLoader.GetString(nameof(AppStrings.PasswordUpperCase));
            VEHICLE_VIN_CODE_PLACEHOLDER_STEP = ResourceLoader.GetString(nameof(AppStrings.VINCodeUpperCase));
            REGISTRATION_DATE_PLACEHOLDER_STEP = ResourceLoader.GetString(nameof(AppStrings.DateOfCreationUppercase));
        }

        protected virtual void ResetValidationObjects() {
            MainInput = _validationObjectFactory.GetValidatableObject<string>();
            MainInput.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = ResourceLoader.GetString(nameof(AppStrings.FieldRequired)) });
        }

        protected abstract void OnStepCommand();
    }
}
