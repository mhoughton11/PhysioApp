using System;
using System.Collections.Generic;
using Autofac;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.ViewModels.Profile;
using Xamarin.Forms;

namespace DIYHIIT.Views.Profile
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
