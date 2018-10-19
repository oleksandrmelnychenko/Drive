using Drive.Client.ViewModels.Base;

namespace Drive.Client.ViewModels.IdentityAccounting {
    public sealed class UnauthorizeViewModel : NestedViewModel {

        string _title = "UnauthorizeViewModel";
        public string Title {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        ///     ctor().
        /// </summary>
        public UnauthorizeViewModel() {

        }
    }
}
