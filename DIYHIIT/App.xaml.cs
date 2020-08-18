using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Utility;
using DIYHIIT.Views;
using DLToolkit.Forms.Controls;
using System;
using System.IO;
using Xamarin.Forms;

namespace DIYHIIT
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            InitializeApp();

            InitalizeNavigation();            
        }

        private async void InitalizeNavigation()
        {
            var navigationService = AppContainer.Resolve<INavigationService>();
            await navigationService.InitializeAsync();

            MainPage = new MainPage();
        }

        private void InitializeApp()
        {
            FlowListView.Init();
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
