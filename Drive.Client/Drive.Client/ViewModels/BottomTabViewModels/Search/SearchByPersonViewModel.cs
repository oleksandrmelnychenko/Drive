﻿using Drive.Client.Factories.Validation;
using Drive.Client.Helpers;
using Drive.Client.Helpers.Localize;
using Drive.Client.Models.Identities.NavigationArgs;
using Drive.Client.Resources.Resx;
using Drive.Client.Validations;
using Drive.Client.Validations.ValidationRules;
using Drive.Client.ViewModels.Base;
using Drive.Client.ViewModels.Search;
using Drive.Client.Views.BottomTabViews.Search;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.ViewModels.BottomTabViewModels.Search {
    public class SearchByPersonViewModel : NestedViewModel, IVisualFiguring {

        public Type RelativeViewType => typeof(SearchByPersonView);

        StringResource _tabHeader = ResourceLoader.Instance.GetString(nameof(AppStrings.PersonUpperCase));
        public StringResource TabHeader {
            get => _tabHeader;
            private set => SetProperty(ref _tabHeader, value);
        }

        bool _visibilityClosedView;
        public bool VisibilityClosedView {
            get { return _visibilityClosedView; }
            set { SetProperty(ref _visibilityClosedView, value); }
        }
      
        ValidatableObject<string> _lastName;
        public ValidatableObject<string> LastName {
            get => _lastName;
            private set => SetProperty(ref _lastName, value);
        }
        
        public ICommand InputCompleteCommand => new Command(async () => await OnInputComplete());

        /// <summary>
        ///     ctor().
        /// </summary>
        public SearchByPersonViewModel(IValidationObjectFactory validationObjectFactory) {
            _lastName = validationObjectFactory.GetValidatableObject<string>();

            AddValidations();
        }

       
        public override Task InitializeAsync(object navigationData) {
            UpdateView();

            return base.InitializeAsync(navigationData);
        }

        private async Task OnInputComplete() {
            if (Validate()) {
                SearchByPersonArgs searchByPersonArgs = new SearchByPersonArgs { lastName = LastName.Value };
                await NavigationService.NavigateToAsync<SearchByPersonSecondStepViewModel>(searchByPersonArgs);
            }
        }

        private bool Validate() {
            bool isValidLastName = LastName.Validate();

            return isValidLastName;
        }

        private void UpdateView() {
            VisibilityClosedView = !BaseSingleton<GlobalSetting>.Instance.UserProfile.IsAuth;
        }

        private void AddValidations() {
            _lastName.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = ValidatableObject<string>.FIELD_IS_REQUIRED_VALIDATION_MESSAGE });
        }
    }
}
