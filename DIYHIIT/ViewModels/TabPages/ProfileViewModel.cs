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
using DIYHIIT.ViewModels.Profile;

using static DIYHIIT.Constants.Messages;
using System.Collections.ObjectModel;
using Syncfusion.XForms.ProgressBar;

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

            Task.Run(async () => await InitializeAsync(null));
        }

        #region Private Fields

        // View Components
        private string _userName;
        private string _userImage;
        private string _userEmail;

        // Progress Bar colours
        private double _emptyStart;
        private double _emptyEnd;

        private double _hiitStart;
        private double _hiitEnd;

        private double _caliStart;
        private double _caliEnd;

        private double _yogaStart;
        private double _yogaEnd;

        private double _mobStart;
        private double _mobEnd;

        private double _pilaStart;
        private double _pilaEnd;

        private double _stretchStart;
        private double _stretchEnd;

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

        #region Progress Bar Colour Bindings

        public double HIITStart
        {
            get => _hiitStart;
            set
            {
                _hiitStart = value;
                RaisePropertyChanged(() => HIITStart);
            }
        }

        public double HIITEnd
        {
            get => _hiitEnd;
            set
            {
                _hiitEnd = value;
                RaisePropertyChanged(() => HIITEnd);
            }
        }

        public double CaliStart
        {
            get => _caliStart;
            set
            {
                _caliStart = value;
                RaisePropertyChanged(() => CaliStart);
            }
        }

        public double CaliEnd
        {
            get => _caliEnd;
            set
            {
                _caliEnd = value;
                RaisePropertyChanged(() => CaliEnd);
            }
        }

        public double YogaStart
        {
            get => _yogaStart;
            set
            {
                _yogaStart = value;
                RaisePropertyChanged(() => YogaStart);
            }
        }

        public double YogaEnd
        {
            get => _yogaEnd;
            set
            {
                _yogaEnd = value;
                RaisePropertyChanged(() => YogaEnd);
            }
        }

        public double MobStart
        {
            get => _mobStart;
            set
            {
                _mobStart = value;
                RaisePropertyChanged(() => MobStart);
            }
        }

        public double MobEnd
        {
            get => _mobEnd;
            set
            {
                _mobEnd = value;
                RaisePropertyChanged(() => MobEnd);
            }
        }

        public double StretchStart
        {
            get => _stretchStart;
            set
            {
                _stretchStart = value;
                RaisePropertyChanged(() => StretchStart);
            }
        }

        public double StretchEnd
        {
            get => _stretchEnd;
            set
            {
                _stretchEnd = value;
                RaisePropertyChanged(() => StretchEnd);
            }
        }

        public double PilaStart
        {
            get => _pilaStart;
            set
            {
                _pilaStart = value;
                RaisePropertyChanged(() => PilaStart);
            }
        }

        public double PilaEnd
        {
            get => _pilaEnd;
            set
            {
                _pilaEnd = value;
                RaisePropertyChanged(() => PilaEnd);
            }
        }

        public double EmptyStart
        {
            get => _emptyStart;
            set
            {
                _emptyStart = value;
                RaisePropertyChanged(() => EmptyStart);
            }
        }

        public double EmptyEnd
        {
            get => _emptyEnd;
            set
            {
                _emptyEnd = value;
                RaisePropertyChanged(() => EmptyEnd);
            }
        }

        #endregion

        public ICommand SettingsTapped => new Command(OnSettingsTapped);

        #endregion

        #region Public Methods

        public override Task InitializeAsync(object data)
        {
            MessagingCenter.Subscribe<EditProfileViewModel>(this, ProfileUpdated, (sender) =>
            {
                GetProfile();
            });

            GetProfile();

            HIITStart = 0;
            HIITEnd = 25;

            CaliStart = 25;
            CaliEnd = 50;

            YogaStart = 50;
            YogaEnd = 60;

            PilaStart = 60;
            PilaEnd = 75;

            MobStart = 75;
            MobEnd = 95;

            StretchStart = 95;
            StretchEnd = 100;

            return base.InitializeAsync(data);
        }

        #endregion

        #region Private Methods

        private void GetProfile()
        {
            UserName = App.CurrentUser.FirstName + " " + App.CurrentUser.Surname;
            UserImage = "https://icons-for-free.com/iconfiles/png/512/neutral+user-131964784832104677.png";
            UserEmail = App.CurrentUser.Username;
        }

        private async void OnSettingsTapped()
        {
            await _navigation.PushAsync(new SettingsView(), true);
        }

        #endregion
    }
}
