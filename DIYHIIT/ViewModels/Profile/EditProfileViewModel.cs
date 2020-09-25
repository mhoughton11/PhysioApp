using System;
using System.Threading.Tasks;
using System.Windows.Input;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.ViewModels.Base;
using Xamarin.Forms;
using static DIYHIIT.Constants.Messages;

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

            Task.Run(() => InitializeAsync(null));
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

        #region Public Methods

        public override Task InitializeAsync(object data)
        {
            FirstName = App.CurrentUser.FirstName;
            Surname = App.CurrentUser.Surname;
            Height = App.CurrentUser.Height.GetValueOrDefault().ToString();
            Weight = App.CurrentUser.Weight.GetValueOrDefault().ToString();

            return base.InitializeAsync(data);
        }

        #endregion

        #region Private Methods

        private async void OnSaveChanges()
        {
            _dialogService.ShowLoading("Saving changes...");

            if (FirstName != null)
            {
                App.CurrentUser.FirstName = FirstName;
            }

            if (Surname != null)
            {
                App.CurrentUser.Surname = Surname;
            }

            if (Weight != null)
            {
                App.CurrentUser.Weight = double.Parse(Weight);
            }

            if (Height != null)
            {
                App.CurrentUser.Height = double.Parse(Height);
            }

            await _userDataService.UpdateUser(App.CurrentUser);

            MessagingCenter.Send(this, ProfileUpdated);

            _dialogService.HideLoading();

            await _navigation.PopToRootAsync();
        }

        #endregion
    }
}
