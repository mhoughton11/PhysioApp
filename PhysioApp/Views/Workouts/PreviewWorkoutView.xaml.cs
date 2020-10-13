using Autofac;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Contracts.Services.General;
using PhysioApp.DependencyInjection;
using PhysioApp.Library.Models;
using PhysioApp.ViewModels;
using PhysioApp.ViewModels.Workouts;
using Xamarin.Forms;

namespace PhysioApp.Views.Workouts
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
