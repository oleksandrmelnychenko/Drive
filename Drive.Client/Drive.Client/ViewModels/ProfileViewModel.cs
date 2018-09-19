using Drive.Client.Helpers;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.ViewModels {
    public sealed class ProfileViewModel : NestedViewModel, IBottomBarTab {

        public bool IsBudgeVisible { get; private set; }

        public int BudgeCount { get; private set; }

        public string TabHeader { get; private set; }

        public string TabIcon { get; private set; } = IconPath.PROFILE;

        public Type RelativeViewType { get; private set; } = typeof(ProfileView);

        public bool HasBackgroundItem => false;

        /// <summary>
        ///     ctor().
        /// </summary>
        public ProfileViewModel() {

        }
    }
}
