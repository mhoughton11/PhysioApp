using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.Views;
using DLToolkit.Forms.Controls;
using Prism;
using Prism.Ioc;
using Prism.Unity;
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

            MainPage = new MainPage();
        }

        private void InitializeApp()
        {
            FlowListView.Init();
            AppContainer.RegisterDependancies();
        }
    }
}
