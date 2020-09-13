using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.Library.Models;
using DIYHIIT.Views;
using DLToolkit.Forms.Controls;
using System;
using System.IO;
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

            MainPage = new LoginView();
        }

        private void InitializeApp(AppSetup setup)
        {
            AppHostOptions = HostOptions.Production;

            AppContainer.Container = setup.CreateContainer();
            FlowListView.Init();
        }
    }
}
