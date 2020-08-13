using System;
using DIYHIIT.Contracts.Services.General;

namespace DIYHIIT.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(INavigationService navigationService,
                              IAuthenticationService authenticationService,
                              IDialogService dialogService)
            : base(navigationService, dialogService)
        {
            LabelText = "Login page!";
        }

        private string _labelText;
        public string LabelText
        {
            get => _labelText;
            set
            {
                _labelText = value;
                RaisePropertyChanged(() => LabelText);
            }
        }
    }
}
