using System;
using System.Collections.Generic;
using Autofac;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.ViewModels;
using DIYHIIT.ViewModels.Authentication;
using Xamarin.Forms;

namespace DIYHIIT.Views
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
