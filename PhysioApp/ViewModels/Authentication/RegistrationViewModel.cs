using System;
using System.Windows.Input;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Contracts.Services.General;
using PhysioApp.Library.Models;
using PhysioApp.ViewModels.Base;
using PhysioApp.Views;
using Xamarin.Forms;

namespace PhysioApp.ViewModels.Authentication
{
    public class RegistrationViewModel : BaseViewModel
    {
        public RegistrationViewModel(IUserDataService userDataService,
                                     IAuthenticationService authenticationService,
                                     INavigation navigation,
                                     IDialogService dialogService)
            : base(navigation, dialogService)
        {
            _userDataService = userDataService;
            _authenticationService = authenticationService;
        }

        #region Private Fields

        // View Components
        private string _userEmail;
        private string _userPassword;

        // Model Components
        private readonly IUserDataService _userDataService;
        private readonly IAuthenticationService _authenticationService;

        #endregion

        #region Public Members and Commands

        public string UserEmail
        {
            get => _userEmail;
            set
            {
                _userEmail = value;
                RaisePropertyChanged(() => UserEmail);
            }
        }

        public string UserPassword
        {
            get => _userPassword;
            set
            {
                _userPassword = value;
                RaisePropertyChanged(() => UserPassword);
            }
        }

        public ICommand RegisterCommand => new Command(OnRegisterCommand);

        #endregion

        #region Private Methods

        private async void OnRegisterCommand()
        {
            _dialogService.ShowLoading("Signing up...");

            string email, password;

            // Make sure inputted values are valid
            if (UserEmail == null)
            {
                await _dialogService.ShowAlertAsync("Please enter a valid address.", "Email empty");
                return;
            }
            else
            {
                email = UserEmail;
            }

            if (UserPassword == null)
            {
                await _dialogService.ShowAlertAsync("Please enter a valid address.", "Email empty");
                return;
            }
            else
            {
                password = UserEmail;
            }

            // Save user into Firebase with auth service
            var response = await _authenticationService.SignUpWithEmailAndPassword(email, password);

            if (response != null)
            {
                var user = new User()
                {
                    Uid = response.UserUid,
                    Username = email
                };

                // Create new user in PhysioApp database
                var result = await _userDataService.SaveUser(user);

                // If all steps successful, set current user as result and navigate to new home page.
                if (result != null)
                {
                    _dialogService.HideLoading();

                    App.CurrentUser = result;
                    App.Current.MainPage = new MainPage();
                }
            }

            _dialogService.HideLoading();
        }

        #endregion
    }
}
