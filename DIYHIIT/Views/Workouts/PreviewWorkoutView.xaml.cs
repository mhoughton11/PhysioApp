using Autofac;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.Library.Models;
using DIYHIIT.ViewModels;
using DIYHIIT.ViewModels.Workouts;
using Xamarin.Forms;

namespace DIYHIIT.Views.Workouts
{
    public partial class PreviewWorkoutView : ContentPage
    {
        PreviewWorkoutViewModel viewModel;

        public PreviewWorkoutView(Workout workout)
        {
            InitializeComponent();

            var dialogService = AppContainer.Container.Resolve<IDialogService>();
            var exerciseDataService = AppContainer.Container.Resolve<IExerciseDataService>();

            BindingContext = viewModel = new PreviewWorkoutViewModel(workout, exerciseDataService, Navigation, dialogService);
        }
    }
}
