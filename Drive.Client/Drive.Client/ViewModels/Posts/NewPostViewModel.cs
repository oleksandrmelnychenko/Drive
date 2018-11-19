using Drive.Client.Models.Identities.Posts;
using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.Posts {
    internal sealed class NewPostViewModel : ContentPageBaseViewModel {

        /// <summary>
        ///     ctor().
        /// </summary>
        public NewPostViewModel() {
            ActionBarViewModel = DependencyLocator.Resolve<NewPostActionBarViewModel>();

        }

        public override void Dispose() {
            base.Dispose();


        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is PostType postType) {

            }

            return base.InitializeAsync(navigationData);
        }
    }
}
