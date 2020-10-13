using Autofac;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Contracts.Services.General;
using PhysioApp.DependencyInjection;
using PhysioApp.ViewModels;
using PhysioApp.ViewModels.Tabs;
using Xamarin.Forms;

namespace PhysioApp.Views.Tabs
{
    public partial class ProfileView : ContentPage
    {
        ProfileViewModel viewModel;

        public ProfileView()
        {
            InitializeComponent();

            var dialog = AppContainer.Container.Resolve<IDialogService>();
            var authentication = AppContainer.Container.Resolve<IAuthenticationService>();
            var users = AppContainer.Container.Resolve<IUserDataService>();

            BindingContext = viewModel = new ProfileViewModel(Navigation, dialog, authentication, users);
        }
    }
}
