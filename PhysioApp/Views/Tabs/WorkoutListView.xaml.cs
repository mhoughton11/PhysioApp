using Autofac;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Contracts.Services.General;
using PhysioApp.DependencyInjection;
using PhysioApp.ViewModels;
using PhysioApp.ViewModels.Tabs;
using Xamarin.Forms;

namespace PhysioApp.Views.Tabs
{
    public partial class WorkoutListView : ContentPage
    {
        WorkoutListViewModel viewModel;

        public WorkoutListView()
        {
            InitializeComponent();

            var workoutDataService = AppContainer.Container.Resolve<IWorkoutDataService>();
            var exerciseDataService = AppContainer.Container.Resolve<IExerciseDataService>();

            var dialogService = AppContainer.Container.Resolve<IDialogService>();

            BindingContext = viewModel = new WorkoutListViewModel(0, workoutDataService, exerciseDataService, Navigation, dialogService);
        }
    }
}
