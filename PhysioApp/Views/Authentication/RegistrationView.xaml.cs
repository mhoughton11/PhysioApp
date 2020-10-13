using System;
using System.Collections.Generic;
using Autofac;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Contracts.Services.General;
using PhysioApp.DependencyInjection;
using PhysioApp.ViewModels.Authentication;
using Xamarin.Forms;

namespace PhysioApp.Views.Authentication
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
