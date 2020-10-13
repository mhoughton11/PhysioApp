using System;
using System.Collections.Generic;
using Autofac;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Contracts.Services.General;
using PhysioApp.DependencyInjection;
using PhysioApp.ViewModels.Profile;
using Xamarin.Forms;

namespace PhysioApp.Views.Profile
{
    public partial class EditProfileView : ContentPage
    {
        EditProfileViewModel viewModel; 

        public EditProfileView()
        {
            InitializeComponent();

            var userDataService = AppContainer.Container.Resolve<IUserDataService>();
            var dialogService = AppContainer.Container.Resolve<IDialogService>();

            BindingContext = viewModel = new EditProfileViewModel(userDataService, Navigation, dialogService);
        }
    }
}
