using Autofac;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Contracts.Services.General;
using PhysioApp.DependencyInjection;
using PhysioApp.Library.Models;
using PhysioApp.Views;
using PhysioApp.Views.Authentication;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using static PhysioApp.Constants.CacheNameConstants;
using static PhysioApp.Library.Settings.Settings;

namespace PhysioApp
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
