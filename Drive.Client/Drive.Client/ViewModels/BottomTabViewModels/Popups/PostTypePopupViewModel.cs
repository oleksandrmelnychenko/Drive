using Drive.Client.DataItems.Posts;
using Drive.Client.Models.DataItems.SelectPostTypes;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.Posts;
using Drive.Client.Views.BottomTabViews.Popups;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels.Popups {
    public class PostTypePopupViewModel : PopupBaseViewModel {

        private readonly IPostTypeDataItems _postTypeDataItems;

        public PostTypePopupViewModel(IPostTypeDataItems postTypeDataItems) {
            _postTypeDataItems = postTypeDataItems;

            PostTypes = _postTypeDataItems.BuildLanguageDataItems(ResourceLoader);
        }

        public ICommand CancelCommand => new Command(() => ClosePopupCommand.Execute(null));

        public ICommand SelectPostTypesCommand => new Command((param) => OnSelectPostTypes(param));

        List<PostTypeDataItem> _postTypes;
        public List<PostTypeDataItem> PostTypes {
            get { return _postTypes; }
            set { SetProperty(ref _postTypes, value); }
        }

        public override Type RelativeViewType => typeof(PostTypePopupView);

        private void OnSelectPostTypes(object param) {
            if (param is PostTypeDataItem postTypeDataItem) {
                ClosePopupCommand.Execute(null);

                NavigationService.NavigateToAsync<NewPostViewModel>(postTypeDataItem.PostType);
            }
        }
    }
}
