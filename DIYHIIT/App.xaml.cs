using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
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

        public App()
        {
            InitializeComponent();

            InitializeApp();

            MainPage = new MainPage();
        }

        private void InitializeApp()
        {
            AppHostOptions = HostOptions.LocalHost;

            FlowListView.Init();
            AppContainer.RegisterDependancies();
        }
    }
}
