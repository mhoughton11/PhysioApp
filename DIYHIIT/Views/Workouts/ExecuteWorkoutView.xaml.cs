using System.Collections.Generic;
using Autofac;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Models;
using DIYHIIT.ViewModels;
using DIYHIIT.ViewModels.Workouts;
using Xamarin.Forms;

namespace DIYHIIT.Views.Workouts
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

            BindingContext = viewModel = new ExecuteWorkoutViewModel(workout,
                    exercises,
                    feedItemService,
                    workoutDataService,
                    userDataService,
                    Navigation,
                    dialog);
        }
    }
}