using Autofac;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Contracts.Services.General;
using PhysioApp.DependencyInjection;
using PhysioApp.ViewModels;
using PhysioApp.ViewModels.Tabs;
using Xamarin.Forms;

namespace PhysioApp.Views.Tabs
{
    public partial class HomeView : ContentPage
    {
        HomeViewModel viewModel;

        public HomeView()
        {
            InitializeComponent();

            var dialog = AppContainer.Container.Resolve<IDialogService>();
            var userDataService = AppContainer.Container.Resolve<IUserDataService>();
            var feedItemService = AppContainer.Container.Resolve<IFeedItemService>();

            BindingContext = viewModel = new HomeViewModel(userDataService, feedItemService, Navigation, dialog);
        }
    }
}
