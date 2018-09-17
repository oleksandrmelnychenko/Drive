using Drive.Client.Services.RequestProvider;
using Drive.Client.ViewModels.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels {
    public sealed class MainViewModel : ViewModelBase {

        /// <summary>
        ///     ctor().
        /// </summary>
        public MainViewModel() {

        }

        public override Task InitializeAsync(object navigationData) {


            return base.InitializeAsync(navigationData);
        }
    }
}
