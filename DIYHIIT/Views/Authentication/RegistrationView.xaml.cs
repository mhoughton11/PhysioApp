using System;
using System.Collections.Generic;
using Autofac;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.ViewModels.Authentication;
using Xamarin.Forms;

namespace DIYHIIT.Views.Authentication
{
    public partial class RegistrationView : ContentPage
    {
        RegistrationViewModel viewModel;

        public RegistrationView()
        {
            InitializeComponent();

            var user = AppContainer.Container.Resolve<IUserDataService>();
            var auth = AppContainer.Container.Resolve<IAuthenticationService>();
            var dialog = AppContainer.Container.Resolve<IDialogService>();

            BindingContext = viewModel = new RegistrationViewModel(user, auth, Navigation, dialog);
        }
    }
}
