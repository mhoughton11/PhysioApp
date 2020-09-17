using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using Xamarin.Forms;

namespace DIYHIIT.Views.Authentication
{
    public partial class SplashScreen : ContentPage
    {
        public SplashScreen()
        {
            InitializeComponent();

            _authenticationService = AppContainer.Container.Resolve<IAuthenticationService>();
            _userDataService = AppContainer.Container.Resolve<IUserDataService>();

            //AttemptAutoLogin();
        }

        private readonly IAuthenticationService _authenticationService;
        private readonly IUserDataService _userDataService;

        public async void AttemptAutoLogin()
        {
            try
            {
                var response = _authenticationService.AutoLogin();

                if (!response.IsAuthenticated)
                {
                    Debug.WriteLine($"firebase auto-login failed");
                    App.Current.MainPage = new LoginView();
                    return;
                }

                // Success
                var user = await _userDataService.GetUser(response.UserUid);

                if (user != null)
                {
                    Debug.WriteLine($"Auto login success: {user.Username}");

                    App.CurrentUser = user;
                    App.Current.MainPage = new MainPage();
                    return;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            Debug.WriteLine($"DB user retrieval failed");
            App.Current.MainPage = new LoginView();
        }
    }
}
