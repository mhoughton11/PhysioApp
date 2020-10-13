using System.Collections.Generic;
using Autofac;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Contracts.Services.General;
using PhysioApp.DependencyInjection;
using PhysioApp.Library.Contracts;
using PhysioApp.Library.Models;
using PhysioApp.ViewModels;
using PhysioApp.ViewModels.Workouts;
using Xamarin.Forms;

namespace PhysioApp.Views.Workouts
{
    public partial class ExecuteWorkoutView : ContentPage
    {
        ExecuteWorkoutViewModel viewModel;

        public ExecuteWorkoutView(Workout workout, List<IExercise> exercises)
        {
            InitializeComponent();

            var dialog = AppContainer.Container.Resolve<IDialogService>();
            var userDataService = AppContainer.Container.Resolve<IUserDataService>();
            var feedItemService = AppContainer.Container.Resolve<IFeedItemService>();
            var workoutDataService = AppContainer.Container.Resolve<IWorkoutDataService>();
            var auditTrailDataService = AppContainer.Container.Resolve<IAuditTrailDataService>();

            BindingContext = viewModel = new ExecuteWorkoutViewModel(workout,
                    exercises,
                    feedItemService,
                    workoutDataService,
                    userDataService,
                    auditTrailDataService,
                    Navigation,
                    dialog);
        }
    }
}