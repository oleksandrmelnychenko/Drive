using Drive.Client.Models.Identities.Posts;
using Drive.Client.ViewModels.ActionBars;
using Drive.Client.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.Posts {
    internal sealed class NewPostViewModel : ContentPageBaseViewModel {

        public NewPostViewModel() {
            ActionBarViewModel = DependencyLocator.Resolve<NewPostActionBarViewModel>();

            AttachedPostMedias.Add(new AttachedPostMedia());
            AttachedPostMedias.Add(new AttachedPostMedia());
            AttachedPostMedias.Add(new AttachedPostMedia());
            AttachedPostMedias.Add(new AttachedPostMedia());
            AttachedPostMedias.Add(new AttachedPostMedia());
            AttachedPostMedias.Add(new AttachedPostMedia());
            AttachedPostMedias.Add(new AttachedPostMedia());
            AttachedPostMedias.Add(new AttachedPostMedia());
        }

        private PostType _targetPostType;
        public PostType TargetPostType {
            get => _targetPostType;
            private set => SetProperty<PostType>(ref _targetPostType, value);
        }

        private ObservableCollection<AttachedPostMedia> _attachedPostMedias = new ObservableCollection<AttachedPostMedia>();
        public ObservableCollection<AttachedPostMedia> AttachedPostMedias {
            get => _attachedPostMedias;
            private set => SetProperty<ObservableCollection<AttachedPostMedia>>(ref _attachedPostMedias, value);
        }

        private AttachedPostMedia _selectedAttachedPostMedia;
        public AttachedPostMedia SelectedAttachedPostMedia {
            get => _selectedAttachedPostMedia;
            set => SetProperty<AttachedPostMedia>(ref _selectedAttachedPostMedia, value);
        }

        public override Task InitializeAsync(object navigationData) {

            if (navigationData is PostType postTypeNavigationData) {
                TargetPostType = postTypeNavigationData;
            }

            return base.InitializeAsync(navigationData);
        }
    }
}
