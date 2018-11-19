using Drive.Client.DataItems.Posts;
using Drive.Client.Models.DataItems.SelectPostTypes;
using Drive.Client.Models.Identities.Posts;
using Drive.Client.ViewModels.Base;
using Drive.Client.Views.BottomTabViews.Popups;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels.Popups {
    public class PostTypePopupViewModel : PopupBaseViewModel {

        private readonly IPostTypeDataItems _postTypeDataItems;

        List<PostTypeDataItem> _postTypes;
        public List<PostTypeDataItem> PostTypes {
            get { return _postTypes; }
            set { SetProperty(ref _postTypes, value); }
        }

        public override Type RelativeViewType => typeof(PostTypePopupView);

        public ICommand CancelCommand => new Command(() => ClosePopupCommand.Execute(null));

        public ICommand SelectPostTypesCommand => new Command((param) => OnSelectPostTypes(param));

        /// <summary>
        ///     ctor().
        /// </summary>
        public PostTypePopupViewModel(IPostTypeDataItems postTypeDataItems) {
            _postTypeDataItems = postTypeDataItems;

            PostTypes = _postTypeDataItems.BuildLanguageDataItems();
        }

        private void OnSelectPostTypes(object param) {
            if (param is PostTypeDataItem postTypeDataItem) {
                if (postTypeDataItem.PostType == PostType.MediaPost) {
                    Debug.WriteLine("In developing..");
                } else if (postTypeDataItem.PostType == PostType.TextPost) {
                    Debug.WriteLine("In developing..");
                }
                ClosePopupCommand.Execute(null);
            }
        }
    }
}
