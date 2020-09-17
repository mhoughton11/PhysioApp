using Autofac;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.Library.Models;
using DIYHIIT.Views;
using DIYHIIT.Views.Authentication;
using DLToolkit.Forms.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using static DIYHIIT.Library.Settings.Settings;

namespace DIYHIIT
{
    public partial class App : Application
    {
        public static HostOptions AppHostOptions;
        public static User CurrentUser;

        public App(AppSetup setup)
        {
            InitializeComponent();

            InitializeApp(setup);

            MainPage = new SplashScreen();

            //AttemptAutoLogin();
        }

        private void InitializeApp(AppSetup setup)
        {
            AppHostOptions = HostOptions.Production;

            AppContainer.Container = setup.CreateContainer();
            FlowListView.Init();
        }

        private async void AttemptAutoLogin()
        {
            var _authenticationService = AppContainer.Container.Resolve<IAuthenticationService>();
            var _userDataService = AppContainer.Container.Resolve<IUserDataService>();

            try
            {
                var response = _authenticationService.AutoLogin();

                if (!response.IsAuthenticated)
                {
                    Debug.WriteLine($"firebase auto-login failed");
                    MainPage = new LoginView();
                    return;
                }
                // Success
                var user = await _userDataService.GetUser(response.UserUid);

                if (user != null)
                {
                    Debug.WriteLine($"Auto login success: {user.Username}");

                    CurrentUser = user;
                    MainPage = new MainPage();
                    return;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("DB user retrieval failed");
                Debug.WriteLine(e.Message);
            }

            MainPage = new LoginView();
        }
    }
}
