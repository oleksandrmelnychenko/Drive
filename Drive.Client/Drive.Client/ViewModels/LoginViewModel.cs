using Drive.Client.Helpers;
using Drive.Client.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels {
    public sealed class LoginViewModel : ViewModelBase {

        public LoginViewModel() {
            
        }

        public override Task InitializeAsync(object navigationData) {
            BaseSingleton<GlobalSetting>.Instance.UserProfile.AccesToken = "1111";

            return base.InitializeAsync(navigationData);
        }
    }
}
