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
using DIYHIIT.Views.Profile;

namespace DIYHIIT.ViewModels.Tabs
{
    public class ProfileViewModel : BaseViewModel
    {
        public ProfileViewModel(INavigation navigation,
                                IDialogService dialogService,
                                IAuthenticationService authenticationService,
                                IUserDataService userDataService)
            : base(navigation, dialogService)
        {
            _authenticationService = authenticationService;
            _userDataService = userDataService;

        }

        #region Private Fields

        // View Components
        private string _userName;
        private string _userImage;
        private string _userEmail;

        // Model Components
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserDataService _userDataService;

        #endregion

        #region Public Members and Commands

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public string UserImage
        {
            get => _userImage;
            set
            {
                _userImage = value;
                RaisePropertyChanged(() => UserImage);
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

        public ICommand SettingsTapped => new Command(OnSettingsTapped);

        #endregion

        #region Public Methods

        public override Task InitializeAsync(object data)
        {
            UserName = App.CurrentUser.FirstName + " "
                + App.CurrentUser.LastName;

            UserImage = "";

            return base.InitializeAsync(data);
        }

        #endregion

        #region Private Methods

        private async void OnSettingsTapped()
        {
            await _navigation.PushAsync(new SettingsView());
        }

        #endregion
    }
}
