using Autofac;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.ViewModels;
using DIYHIIT.ViewModels.Tabs;
using Xamarin.Forms;

namespace DIYHIIT.Views.Tabs
{
    public partial class HomeView : ContentPage
    {
        HomeViewModel viewModel;

        public HomeView()
        {
            InitializeComponent();

            var dialog = AppContainer.Container.Resolve<IDialogService>();
            var workoutDataService = AppContainer.Container.Resolve<IWorkoutDataService>();

            BindingContext = viewModel = new HomeViewModel(workoutDataService, Navigation, dialog);
        }
    }
}
