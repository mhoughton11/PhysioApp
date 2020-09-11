using Autofac;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.ViewModels;
using Xamarin.Forms;

namespace DIYHIIT.Views
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
