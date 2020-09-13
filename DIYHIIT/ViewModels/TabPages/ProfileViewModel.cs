using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Models;
using DIYHIIT.Views.Authentication;
using DIYHIIT.ViewModels.Base;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels.Tabs
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserDataService _userDataService;
        Random random;

        public string TextLabel { get; set; }

        public ProfileViewModel(INavigation navigation,
                                IDialogService dialogService,
                                IAuthenticationService authenticationService,
                                IUserDataService userDataService)
            : base(navigation, dialogService)
        {
            TextLabel = "Profile";
            _authenticationService = authenticationService;
            _userDataService = userDataService;
            random = new Random();
        }

        public override Task InitializeAsync(object data)
        {
            random = new Random();
            TextLabel = "Profile" + random.Next(0xFF);

            return base.InitializeAsync(data);
        }

        public ICommand LogoutCommand => new Command(OnLogoutCommand);

        #region Private Methods

        private void OnLogoutCommand()
        {
            _authenticationService.SignOut();

            App.CurrentUser = null;
            App.Current.MainPage = new LoginView();
        }

        #endregion
    }
}
