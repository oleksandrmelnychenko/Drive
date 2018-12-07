using Drive.Client.Controls.Stacklist;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Views.Posts {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AtatchedNewPostMediaStackItem : CommonStackListItem {

        public AtatchedNewPostMediaStackItem() {
            InitializeComponent();

            Deselected();
        }

        public async override void Deselected() {
            if (IsOnSelectionVisualChangesEnabled) {
                await _deleteTrigger_AbsoluteLayout.FadeTo(0, 125);
                _deleteTrigger_AbsoluteLayout.InputTransparent = true;
            }
        }

        public async override void Selected() {
            if (IsOnSelectionVisualChangesEnabled) {
                await _deleteTrigger_AbsoluteLayout.FadeTo(1, 125);
                _deleteTrigger_AbsoluteLayout.InputTransparent = false;
            }
        }
    }
}