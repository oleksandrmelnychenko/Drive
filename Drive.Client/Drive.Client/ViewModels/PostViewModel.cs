using Drive.Client.Helpers;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Drive.Client.ViewModels {
    public sealed class PostViewModel : NestedViewModel, IBottomBarTab {

        public bool IsBudgeVisible { get; private set; }

        public int BudgeCount { get; private set; }

        public string TabHeader { get; private set; }

        public string TabIcon { get; private set; } = IconPath.POST;

        public Type RelativeViewType { get; private set; } = typeof(PostView);

        public bool HasBackgroundItem => true;

        /// <summary>
        ///     ctor().
        /// </summary>
        public PostViewModel() {

        }
    }
}
