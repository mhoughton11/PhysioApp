using Autofac;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.ViewModels;
using DIYHIIT.ViewModels.Tabs;
using Xamarin.Forms;

namespace DIYHIIT.Views.Tabs
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await viewModel.InitializeAsync(null);
        }
    }
}
