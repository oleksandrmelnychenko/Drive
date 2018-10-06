namespace Drive.Client.Helpers.Localize {
    public class StringResource : ObservableObject {

        public StringResource(string key, string value) {
            Key = key;
            Value = value;
        }

        public string Key { get; }

        private string _value;
        public string Value {
            get => _value;
            set => SetProperty<string>(ref _value, value);
        }
    }
}
