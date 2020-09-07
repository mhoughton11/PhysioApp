using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.ViewModels;
using Xamarin.Forms;

namespace DIYHIIT.Views
{
    public partial class WorkoutListView : ContentPage
    {
        WorkoutListViewModel viewModel;

        public WorkoutListView()
        {
            InitializeComponent();

            var workoutDataService = AppContainer.Resolve<IWorkoutDataService>();
            var dialogService = AppContainer.Resolve<IDialogService>();

            BindingContext = viewModel = new WorkoutListViewModel(0, workoutDataService, Navigation, dialogService);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.InitializeAsync(null);
        }
    }
}
