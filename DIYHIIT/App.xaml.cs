using Autofac;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.Library.Models;
using DIYHIIT.Views;
using DIYHIIT.Views.Authentication;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using static DIYHIIT.Constants.CacheNameConstants;
using static DIYHIIT.Library.Settings.Settings;

namespace DIYHIIT
{
    public partial class App : Application
    {
        public static HostOptions AppHostOptions;
        public static User CurrentUser;
        public static UserSettings Settings;

        public App(AppSetup setup)
        {
            InitializeComponent();

            InitializeApp(setup);

            MainPage = new SplashScreen();
        }

        private void InitializeApp(AppSetup setup)
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(SyncfusionLicense);

            AppHostOptions = HostOptions.Production;

            AppContainer.Container = setup.CreateContainer();
        }
    }
}
