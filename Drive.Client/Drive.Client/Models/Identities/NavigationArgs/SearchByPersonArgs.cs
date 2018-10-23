namespace Drive.Client.Models.Identities.NavigationArgs {
    public class SearchByPersonArgs {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string DateOfBirth { get; set; }

        public string FullName => $"{LastName} {FirstName} {MiddleName}";
    }
}
