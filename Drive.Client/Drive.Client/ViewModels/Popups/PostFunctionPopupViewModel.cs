using Drive.Client.Services.Announcement;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.BottomTabViewModels.Home.Post;
using Drive.Client.Views.Popups;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.Popups {
    public sealed class PostFunctionPopupViewModel : PopupBaseViewModel {

        private readonly IAnnouncementService _announcementService;

        PostBaseViewModel _selectedPostBaseViewModel;
        public PostBaseViewModel SelectedPostBaseViewModel {
            get { return _selectedPostBaseViewModel; }
            set { SetProperty(ref _selectedPostBaseViewModel, value); }
        }

        public override Type RelativeViewType => typeof(PostFunctionPopupView);

        public ICommand CancelCommand => new Command(() => ClosePopupCommand.Execute(null));

        public ICommand DeleteCommand => new Command(() => OnDelete());

        /// <summary>
        ///     ctor().
        /// </summary>
        public PostFunctionPopupViewModel(IAnnouncementService announcementService) {
            _announcementService = announcementService;
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is PostBaseViewModel postBaseViewModel) {
                SelectedPostBaseViewModel = postBaseViewModel;
            }

            return base.InitializeAsync(navigationData);
        }

        private async  void OnDelete() {
            await _announcementService.DeleteAnnounceAsync(SelectedPostBaseViewModel.Post.AnnounceBody.Id, new CancellationTokenSource());

            ClosePopupCommand.Execute(null);

            if (NavigationService.CurrentViewModelsNavigationStack.Count > 1) {
                await NavigationService.GoBackAsync();
            }
        }
    }
}
