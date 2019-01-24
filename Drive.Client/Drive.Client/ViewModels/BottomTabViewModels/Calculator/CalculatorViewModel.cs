using Drive.Client.Helpers;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews.Calculator;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.BottomTabViewModels.Calculator {
    public sealed class CalculatorViewModel : TabbedViewModelBase {

        public override void Dispose() {
            base.Dispose();
        }

        protected override void TabbViewModelInit() {
            TabIcon = IconPath.CALCULATOR;
            RelativeViewType = typeof(CalculatorView);
            HasBackgroundItem = false;
        }
    }
}
