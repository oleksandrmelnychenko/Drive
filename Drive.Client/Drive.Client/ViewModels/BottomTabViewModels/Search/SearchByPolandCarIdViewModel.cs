using Drive.Client.Factories.Validation;
using Drive.Client.Helpers;
using Drive.Client.Helpers.Localize;
using Drive.Client.Models.Arguments.BottomtabSwitcher;
using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.Resources.Resx;
using Drive.Client.Validations;
using Drive.Client.Validations.ValidationRules;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.Search;
using Drive.Client.Views.BottomTabViews.Search;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels.Search {
    public sealed class SearchByPolandCarIdViewModel : NestedViewModel, IVisualFiguring, ISwitchTab {

        public Type RelativeViewType => typeof(SearchByPolandCarIdView);

        StringResource _tabHeader;
        public StringResource TabHeader {
            get => _tabHeader;
            private set => SetProperty(ref _tabHeader, value);
        }

        ValidatableObject<string> _number;
        public ValidatableObject<string> Number {
            get => _number;
            private set => SetProperty(ref _number, value);
        }

        bool _visibilityClosedView;
        public bool VisibilityClosedView {
            get { return _visibilityClosedView; }
            set { SetProperty(ref _visibilityClosedView, value); }
        }

        public ICommand InputCompleteCommand => new Command(async () => await OnInputComplete());

        /// <summary>
        ///     ctor().
        /// </summary>
        public SearchByPolandCarIdViewModel(IValidationObjectFactory validationObjectFactory) {
            _number = validationObjectFactory.GetValidatableObject<string>();

#if DEBUG
            _number.Value = "sps02218";
#endif
        }

        private async Task OnInputComplete() {
            if (Validate()) {
                SearchByPolandNumberArgs searchByPolandNumberArgs = new SearchByPolandNumberArgs { Number = Number.Value };
                await NavigationService.NavigateToAsync<SearchByPolandCarIdSecondStepViewModel>(searchByPolandNumberArgs);
                Number.Value = string.Empty;
            }
        }

        public override void Dispose() {
            base.Dispose();


        }

        private void UpdateView() {
            VisibilityClosedView = !BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth;
        }

        public override Task InitializeAsync(object navigationData) {
            UpdateView();
            ClearSource();
            if (navigationData is SelectedBottomBarTabArgs) {

            }

            return base.InitializeAsync(navigationData);
        }

        public void ClearAfterTabTap() {
            ClearSource();
        }

        public void TabClicked() { }

        protected override void ResolveStringResources() {
            base.ResolveStringResources();

            TabHeader = ResourceLoader.GetString(nameof(AppStrings.ByPolandNumberUpperCase));
        }

        private void ClearSource() {
            try {
                Number.Value = string.Empty;
                Number.IsValid = true;
                Number.Validations?.Clear();
                AddValidations();
            }
            catch (Exception) {
            }
        }

        private bool Validate() {
            bool validationResult = Number.Validate();

            return validationResult;
        }

        private void AddValidations() {
            _number.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = ResourceLoader.GetString(nameof(AppStrings.FieldRequired)) });
        }
    }
}
