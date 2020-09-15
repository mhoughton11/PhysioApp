using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autofac;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.ViewModels;
using DIYHIIT.ViewModels.Workouts;
using Xamarin.Forms;

namespace DIYHIIT.Views.Workouts
{
    public partial class CreateWorkoutView : ContentPage
    {
        CreateWorkoutViewModel viewModel;

        public CreateWorkoutView()
        {
            InitializeComponent();

            var userDataService = AppContainer.Container.Resolve<IUserDataService>();
            var dialogService = AppContainer.Container.Resolve<IDialogService>();

            BindingContext = viewModel = new CreateWorkoutViewModel(0, userDataService, Navigation, dialogService);
        }
    }
}
 