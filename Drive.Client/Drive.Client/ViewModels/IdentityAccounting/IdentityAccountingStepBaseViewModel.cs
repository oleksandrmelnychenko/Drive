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
        public static readonly string NAME_STEP_REGISTRATION_TITLE = "ВВЕДІТЬ ІМ'Я";
        public static readonly string PASSWORD_STEP_REGISTRATION_TITLE = "ВВЕДІТЬ ПАРОЛЬ";
        public static readonly string PASSWORD_CONFIRM_STEP_REGISTRATION_TITLE = "ПІДТВЕРДІТЬ ПАРОЛЬ";

        public static readonly string PHONE_PLACEHOLDER_STEP_REGISTRATION = "ТЕЛЕФОН";
        public static readonly string NAME_PLACEHOLDER_STEP_REGISTRATION = "ІМ'Я";
        public static readonly string PASSWORD_PLACEHOLDER_STEP_REGISTRATION = "ПАРОЛЬ";

        public static readonly string TODO_INPUT_ICON_STUB = "todo: appropriate icon path";

        public IdentityAccountingStepBaseViewModel() {
            ActionBarViewModel = DependencyLocator.Resolve<IdentityAccountingActionBarViewModel>();
            ((IdentityAccountingActionBarViewModel)ActionBarViewModel).InitializeAsync(this);

            ResetValidationObjects();
        }

        public ICommand StepCommand => new Command(() => OnStepCommand());

        private string _stepTitle;
        public string StepTitle {
            get => _stepTitle;
            protected set => SetProperty<string>(ref _stepTitle, value);
        }

        private string _mainInputIconPath;
        public string MainInputIconPath {
            get => _mainInputIconPath;
            protected set => SetProperty<string>(ref _mainInputIconPath, value);
        }

        private string _mainInputPlaceholder;
        public string MainInputPlaceholder {
            get => _mainInputPlaceholder;
            protected set => SetProperty<string>(ref _mainInputPlaceholder, value);
        }

        private Keyboard _keyboardType = Keyboard.Default;
        public Keyboard KeyboardType {
            get => _keyboardType;
            protected set => SetProperty<Keyboard>(ref _keyboardType, value);
        }

        private bool _isPasswordInput;
        public bool IsPasswordInput {
            get => _isPasswordInput;
            protected set => SetProperty<bool>(ref _isPasswordInput, value);
        }

        private ValidatableObject<string> _mainInput = new ValidatableObject<string>();
        public ValidatableObject<string> MainInput {
            get => _mainInput;
            private set => SetProperty<ValidatableObject<string>>(ref _mainInput, value);
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
            MainInput = new ValidatableObject<string>();
            MainInput.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = ValidatableObject<string>.FIELD_IS_REQUIRED_VALIDATION_MESSAGE });
        }

        protected abstract void OnStepCommand();
    }
}
