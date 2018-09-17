using System.Text.RegularExpressions;

namespace Drive.Client.Validations.ValidationRules {
    public class EmailRule<T> : IValidationRule<T> {

        public string ValidationMessage { get; set; }

        public bool Check(T value) {
            if (value == null)
                return false;

            string validatedValue = value as string;

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(validatedValue);

            return match.Success;
        }
    }
}
