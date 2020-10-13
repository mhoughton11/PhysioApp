using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autofac;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Contracts.Services.General;
using PhysioApp.DependencyInjection;
using PhysioApp.ViewModels;
using PhysioApp.ViewModels.Workouts;
using Xamarin.Forms;

namespace PhysioApp.Views.Workouts
{
    public partial class CreateWorkoutView : ContentPage
    {
        CreateWorkoutViewModel viewModel;

        public CreateWorkoutView()
        {
            InitializeComponent();

            var userDataService = AppContainer.Container.Resolve<IUserDataService>();
            var workoutDataService = AppContainer.Container.Resolve<IWorkoutDataService>();
            var dialogService = AppContainer.Container.Resolve<IDialogService>();

            BindingContext = viewModel = new CreateWorkoutViewModel(0, workoutDataService, userDataService, Navigation, dialogService);
        }
    }
}
 