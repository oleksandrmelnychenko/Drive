using Drive.Client.Controls.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.Popups {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateAppVersionPopupView : SinglePopupViewBase {
        public UpdateAppVersionPopupView() {
            InitializeComponent();
        }
    }
}