using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Models;
using DIYHIIT.ViewModels.Base;
using DIYHIIT.Views;
using DIYHIIT.Views.Authentication;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels.Authentication
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
                Debug.WriteLine("Log in failed.");
            }
        }

        private void OnRegisterCommand()
        {
            App.Current.MainPage = new RegistrationView();
        }

        #endregion

    }
}
