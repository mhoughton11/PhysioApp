using System;

namespace DIYHIIT.ViewModels.Authentication
{
    public class RegistrationViewModel : BaseViewModel
    {
        public RegistrationViewModel(IUserDataService userDataService,
                                     IAuthenticationService authenticationService,
                                     Navigation navigation)
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

        }

        #endregion
    }
}
