﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Contracts.Services.General;
using PhysioApp.Library.Models;
using PhysioApp.ViewModels.Base;
using PhysioApp.Views;
using PhysioApp.Views.Authentication;
using Xamarin.Forms;

namespace PhysioApp.ViewModels.Authentication
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(INavigation navigation,
                              IDialogService dialogService,
                              IAuthenticationService authenticationService,
                              IUserDataService userDataService)
            : base(navigation, dialogService)
        {
            _authenticationService = authenticationService;
            _userDataService = userDataService;

            InitializeAsync(null);
        }

        #region Private Fields

        // View Components
        private string _errorLabelVisible;
        private string _userEmail;
        private string _userPassword;

        // Services
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserDataService _userDataService;

        #endregion

        #region Public Properties and Commands

        public string ErrorLabelVisible
        {
            get => _errorLabelVisible;
            set
            {
                _errorLabelVisible = value;
                RaisePropertyChanged(() => ErrorLabelVisible);
            }
        }

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

        public ICommand LoginCommand => new Command(OnLoginCommand);
        public ICommand RegisterCommand => new Command(OnRegisterCommand);

        #endregion

        #region Public Methods

        public override Task InitializeAsync(object data)
        {
            ErrorLabelVisible = "False";

            return base.InitializeAsync(data);
        }

        #endregion

        #region Private Methods

        private async void OnLoginCommand()
        {
            _dialogService.ShowLoading("Logging in...");

            var response = await _authenticationService.LoginWithEmailAndPassword(UserEmail, UserPassword);

            if (!response.IsAuthenticated)
            {
                Debug.WriteLine("Firebase auth failed.");
                ErrorLabelVisible = "True";
                _dialogService.HideLoading();
                return;
            }

            var user = await _userDataService.GetUser(response.UserUid);

            if (user != null)
            {
                //user.AuthToken = response.ResponseToken;
                App.CurrentUser = user;

                _dialogService.HideLoading();
                App.Current.MainPage = new MainPage();
            }
            else
            {
                _dialogService.HideLoading();
                ErrorLabelVisible = "True";
                Debug.WriteLine("User db retrieval failed.");
            }
        }

        private void OnRegisterCommand()
        {
            App.Current.MainPage = new RegistrationView();
        }

        #endregion

    }
}
