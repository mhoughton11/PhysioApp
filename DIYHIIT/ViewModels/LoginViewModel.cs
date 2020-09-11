using System;
using System.Diagnostics;
using System.Windows.Input;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Views;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginViewModel(INavigation navigation, IDialogService dialogService, IAuthenticationService authenticationService)
            : base(navigation, dialogService)
        {
            _authenticationService = authenticationService;
        }

        #region Public Properties and Commands

        public ICommand LoginCommand => new Command(OnLoginCommand);

        #endregion

        #region Private Methods

        private async void OnLoginCommand()
        {
            var email = "maxh1010@gmail.com";
            var password = "bgnpjgg0";

            var response = await _authenticationService.LoginWithEmailAndPassword(email, password);

            if (response != null)
            {
                App.Current.MainPage = new MainPage();
            }
            else
            {
                await _dialogService.ShowAlertAsync("Please try again.", "Login failed.");
            }
        }

        #endregion

    }
}
