using System;
using System.Collections.Generic;
using Autofac;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.ViewModels;
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

            BindingContext = viewModel = new LoginViewModel(Navigation, dialog, authentication);
        }
    }
}
