using System;
using Xamarin.Forms;
using DIYHIIT.Views;
using DIYHIIT.Data;
using System.IO;
using DLToolkit.Forms.Controls;
using DIYHIIT.Data.Database;
using System.Threading.Tasks;
using DIYHIIT.Services;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Diagnostics;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Reflection;
using DIYHIIT.Utility;
using DIYHIIT.Contracts.Services.General;

namespace DIYHIIT
{
    public partial class App : Application
    {
        static public string FolderPath { get; private set; }

        public App()
        {
            InitializeComponent();
            FlowListView.Init();

            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));            

            InitializeApp();

            InitalizeNavigation();

            MainPage = new MainPage();
        }

        private async void InitalizeNavigation()
        {
            var navigationService = AppContainer.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }

        private void InitializeApp()
        {
            

            AppContainer.RegisterDependancies();
        }

        protected override void OnStart()
        {
 
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}
