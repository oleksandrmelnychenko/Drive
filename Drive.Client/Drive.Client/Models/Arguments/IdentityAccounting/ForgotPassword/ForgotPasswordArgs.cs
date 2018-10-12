namespace Drive.Client.Models.Arguments.IdentityAccounting.ForgotPassword {
    public class ForgotPasswordArgs {
        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public string NewPassword { get; set; }
    }
}
