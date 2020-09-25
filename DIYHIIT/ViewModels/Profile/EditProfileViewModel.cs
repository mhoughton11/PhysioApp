using System;
using System.Windows.Input;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.ViewModels.Base;
using Xamarin.Forms;

namespace DIYHIIT.ViewModels.Profile
{
    public class EditProfileViewModel : BaseViewModel
    {
        public EditProfileViewModel(IUserDataService userDataService,
                                    INavigation navigation,
                                    IDialogService dialogService)
            : base (navigation, dialogService)
        {
            _userDataService = userDataService;
        }

        #region Private Fields

        // View Components
        private string _firstName;
        private string _surname;
        private string _height;
        private string _weight;
        

        // Model Components
        private readonly IUserDataService _userDataService;

        #endregion

        #region Public Members and Commands

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                RaisePropertyChanged(() => FirstName);
            }
        } 

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                RaisePropertyChanged(() => Surname);
            }
        } 

        public string Height
        {
            get => _height;
            set
            {
                _height = value;
                RaisePropertyChanged(() => Height);
            }
        } 

        public string Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                RaisePropertyChanged(() => Weight);
            }
        } 

        public ICommand SaveChanges => new Command(OnSaveChanges);

        #endregion

        #region Private Methods

        private async void OnSaveChanges()
        {
            _dialogService.ShowLoading("Saving changes...");

            App.CurrentUser.FirstName = FirstName;
            App.CurrentUser.Surname = Surname;
            App.CurrentUser.Weight = double.Parse(Weight);
            App.CurrentUser.Height = double.Parse(Height);

            await _userDataService.UpdateUser(App.CurrentUser);

            _dialogService.HideLoading();
        }

        #endregion
    }
}
