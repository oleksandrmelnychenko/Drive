using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.Base {
    public abstract class ExtendedBindableObject : BindableObject {
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null) {
            //if (Equals(storage, value)) return false;
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }
    }
}
