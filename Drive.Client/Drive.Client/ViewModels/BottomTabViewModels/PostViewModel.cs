using Drive.Client.Helpers;
using Drive.Client.Services.Identity;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views;
using Drive.Client.Views.BottomTabViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.BottomTabViewModels {
    public sealed class PostViewModel : NestedViewModel, IBottomBarTab {

        private readonly IIdentityService _identityService;

        public bool IsBudgeVisible { get; private set; }

        public int BudgeCount { get; private set; }

        public string TabHeader { get; private set; }

        public string TabIcon { get; private set; } = IconPath.POST;

        public Type RelativeViewType { get; private set; } = typeof(PostView);

        public bool HasBackgroundItem => true;

        /// <summary>
        ///     ctor().
        /// </summary>
        public PostViewModel(IIdentityService identityService ) {
            _identityService = identityService;
        }

        public override Task InitializeAsync(object navigationData) {

            return base.InitializeAsync(navigationData);
        }           
    }
}
