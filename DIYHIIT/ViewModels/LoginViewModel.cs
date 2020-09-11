using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Models;
using DIYHIIT.Views;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels
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

        public override Task InitializeAsync(object data)
        {
            ErrorLabelVisible = "False";

            return base.InitializeAsync(data);
        }

        public ICommand LoginCommand => new Command(OnLoginCommand);

        #endregion

        #region Private Methods

        private async void OnLoginCommand()
        {
            _dialogService.ShowLoading("Logging in...");

            var response = await _authenticationService.LoginWithEmailAndPassword(UserEmail, UserPassword);
            var user = await _userDataService.GetUser(response.UserUid);

            if (user != null)
            {
                App.CurrentUser = user;

                _dialogService.HideLoading();
                App.Current.MainPage = new MainPage();
            }
            else
            {
                ErrorLabelVisible = "True";
                Debug.WriteLine("Log in failed.");
            }
        }

        #endregion

    }
}
