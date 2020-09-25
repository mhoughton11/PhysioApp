using System;
using System.Windows.Input;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.ViewModels.Base;
using DIYHIIT.Views.Profile;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels.Profile
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel(IUserDataService userDataService,
                                 INavigation navigation,
                                 IDialogService dialogService)
            : base (navigation, dialogService)
        {
            _userDataService = userDataService;
        }

        #region Private Fields

        // View Components


        // Model Components
        private readonly IUserDataService _userDataService;

        #endregion

        #region Public Members and Commands

        public ICommand GoToEditProfile => new Command(OnGoToEditProfile);

        #endregion

        #region Private Methods

        private async void OnGoToEditProfile()
        {
            await _navigation.PushAsync(new EditProfileView());
        }

        #endregion
    }
}
