using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.Library.Models;
using DIYHIIT.ViewModels;
using Xamarin.Forms;

namespace DIYHIIT.Views
{
    public partial class PreviewWorkoutView : ContentPage
    {
        PreviewWorkoutViewModel viewModel;

        public PreviewWorkoutView(Workout workout)
        {
            InitializeComponent();

            var dialogService = AppContainer.Resolve<IDialogService>();
            var exerciseDataService = AppContainer.Resolve<IExerciseDataService>();

            BindingContext = viewModel = new PreviewWorkoutViewModel(workout, exerciseDataService, Navigation, dialogService);
        }
    }
}
