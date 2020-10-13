using System;
using System.Collections.Generic;
using Autofac;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Contracts.Services.General;
using PhysioApp.DependencyInjection;
using PhysioApp.ViewModels;
using PhysioApp.ViewModels.Authentication;
using Xamarin.Forms;

namespace PhysioApp.Views.Authentication
{
    public partial class LoginView : ContentPage
    {
        LoginViewModel viewModel;

        public LoginView()
        {
            InitializeComponent();

            var dialog = AppContainer.Container.Resolve<IDialogService>();
            var authentication = AppContainer.Container.Resolve<IAuthenticationService>();
            var userService = AppContainer.Container.Resolve<IUserDataService>();

            BindingContext = viewModel = new LoginViewModel(Navigation, dialog, authentication, userService);
        }
    }
}
