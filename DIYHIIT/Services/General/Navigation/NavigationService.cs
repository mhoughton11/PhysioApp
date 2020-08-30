using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.ViewModels;
using DIYHIIT.Views;
using Xamarin.Forms;

namespace DIYHIIT.Services.General.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly Dictionary<Type, Type> _mappings;

        protected Application CurrentApplication => Application.Current;

        public NavigationService(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
        }

        public async Task InitializeAsync()
        {
            if (_authenticationService.IsUserAuthenticated())
            {
                Debug.WriteLine("Authenticated. Navigating to home view.");
                await NavigateToAsync<HomeViewModel>();
            }
            else
            {
                await NavigateToAsync<LoginViewModel>();
            }
        }

        public async Task ClearBackStack()
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        public async Task NavigateBackAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();

            if (Application.Current.MainPage != null)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                var navigationPage = Application.Current.MainPage as DIYHIITNavigationPage;

                await navigationPage.PopAsync();
            }
        }

        public virtual Task RemoveLastFromBackStackAsync()
        {
            return Task.FromResult(true);
        }

        public async Task PopToRootAsync()
        {
            if (Application.Current.MainPage is MainPage mainPage)
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, parameter);

            if (page is LoginView || page is RegistrationView)
            {
                CurrentApplication.MainPage = new DIYHIITNavigationPage(page);
            }
            else
            {
                var navigationPage = Application.Current.MainPage as DIYHIITNavigationPage;
                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    Application.Current.MainPage = new DIYHIITNavigationPage(page);
                }
            }

            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        protected Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;

            return page;
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];
        }

        private void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(LoginViewModel), typeof(LoginView));
            _mappings.Add(typeof(HomeViewModel), typeof(HomeView));
            _mappings.Add(typeof(RegistrationViewModel), typeof(RegistrationView));
            _mappings.Add(typeof(AddExerciseViewModel), typeof(AddExerciseView));
            _mappings.Add(typeof(CreateWorkoutViewModel), typeof(CreateWorkoutView));
            _mappings.Add(typeof(ExecuteWorkoutViewModel), typeof(ExecuteWorkoutView));
            _mappings.Add(typeof(PreviewWorkoutViewModel), typeof(PreviewWorkoutView));
            _mappings.Add(typeof(WorkoutListViewModel), typeof(WorkoutListView));
            _mappings.Add(typeof(ProfileViewModel), typeof(ProfileView));
        }
    }
}
