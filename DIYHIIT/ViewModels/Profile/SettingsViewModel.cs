using System;
using System.Threading.Tasks;
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
        private bool _postToFeed;

        // Model Components
        private readonly IUserDataService _userDataService;

        #endregion

        #region Public Members and Commands

        public bool PostToFeed
        {
            get => _postToFeed;
            set
            {
                _postToFeed = true;
                RaisePropertyChanged(() => PostToFeed);
            }
        }

        public ICommand EditProfileTapped => new Command(OnEditProfileTapped);
        public ICommand SaveChanges => new Command(OnSaveChanges);

        #endregion

        #region Public Methods

        public override Task InitializeAsync(object data)
        {
            PostToFeed = App.CurrentUser.UserSettings.PostToFeed;

            return base.InitializeAsync(data);
        }

        #endregion

        #region Private Methods

        private async void OnEditProfileTapped()
        {
            await _navigation.PushAsync(new EditProfileView(), true);
        }

        private async void OnSaveChanges(object obj)
        {
            _dialogService.ShowLoading("Saving changes...");

            // Save selected options into 
            App.CurrentUser.UserSettings.PostToFeed = PostToFeed;

            // Update user database
            await _userDataService.UpdateUser(App.CurrentUser);

            _dialogService.HideLoading();

            await _navigation.PopToRootAsync();
        }

        #endregion
    }
}
