using Drive.Client.Helpers;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews.Calculator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.BottomTabViewModels.Calculator {
    public sealed class CalculatorViewModel : TabbedViewModelBase {

        /// <summary>
        ///     ctor().
        /// </summary>
        public CalculatorViewModel() {

        }

        public override void Dispose() {
            base.Dispose();
        }

        public override Task InitializeAsync(object navigationData) {




            return base.InitializeAsync(navigationData);
        }

        protected override void TabbViewModelInit() {
            TabIcon = IconPath.CALCULATOR;
            RelativeViewType = typeof(CalculatorView);
            HasBackgroundItem = false;
        }
    }
}
