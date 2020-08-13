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

        }
    }
}
