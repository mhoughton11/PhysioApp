using System;
using System.Collections.Generic;
using Autofac;
using PhysioApp.Contracts.Services.General;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.DependencyInjection;
using PhysioApp.ViewModels.Profile;
using Xamarin.Forms;

namespace PhysioApp.Views.Profile
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
