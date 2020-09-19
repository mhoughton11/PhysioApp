using System;
using System.Collections.Generic;
using Autofac;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.ViewModels.Profile;
using Xamarin.Forms;

namespace DIYHIIT.Views.Profile
{
    public partial class SettingsView : ContentPage
    {
        SettingsViewModel viewModel;

        public SettingsView()
        {
            InitializeComponent();

            var dialog = AppContainer.Container.Resolve<IDialogService>();
        
            BindingContext = viewModel = new SettingsViewModel(Navigation, dialog);
        }
    }
}
