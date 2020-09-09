using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.ViewModels;
using Xamarin.Forms;

namespace DIYHIIT.Views
{
    public partial class HomeView : ContentPage
    {
        HomeViewModel viewModel;

        public HomeView()
        {
            InitializeComponent();

            var dialog = AppContainer.Resolve<IDialogService>();
            var workoutDataService = AppContainer.Resolve<IWorkoutDataService>();

            BindingContext = viewModel = new HomeViewModel(workoutDataService, Navigation, dialog);
        }
    }
}
