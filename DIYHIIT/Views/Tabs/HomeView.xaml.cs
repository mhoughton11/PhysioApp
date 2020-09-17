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
            var userDataService = AppContainer.Container.Resolve<IUserDataService>();

            BindingContext = viewModel = new HomeViewModel(userDataService, Navigation, dialog);
        }
    }
}
