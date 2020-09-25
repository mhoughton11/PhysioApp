using System;
using System.Collections.Generic;
using Autofac;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Contracts.Services.Data;
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
            var userDataService = AppContainer.Container.Resolve<IUserDataService>();
            var authenticationService = AppContainer.Container.Resolve<IAuthenticationService>();

            BindingContext = viewModel = new SettingsViewModel(userDataService, authenticationService, Navigation, dialog);
        }
    }
}
