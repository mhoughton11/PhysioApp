using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
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

        private void InitializeApp()
        {
            FlowListView.Init();
            AppContainer.RegisterDependancies();
        }

        private async void InitalizeNavigation()
        {
            var navigationService = AppContainer.Resolve<INavigationService>();
            await navigationService.InitializeAsync();

            MainPage = new MainPage();
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
