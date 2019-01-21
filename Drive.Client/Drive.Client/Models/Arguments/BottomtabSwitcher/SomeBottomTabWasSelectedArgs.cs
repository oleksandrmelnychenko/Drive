using System;

namespace Drive.Client.Models.Arguments.BottomtabSwitcher {
    public class SomeBottomTabWasSelectedArgs {

        public SomeBottomTabWasSelectedArgs(Type selectedTabType) {
            if (selectedTabType.IsClass) { }

            SelectedTabType = selectedTabType;
        }

        public Type SelectedTabType { get; private set; }
    }
}
